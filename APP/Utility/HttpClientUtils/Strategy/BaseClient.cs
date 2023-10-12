namespace APP.Utility.HttpClientUtils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using APP.Utility.Extension;

    public abstract class BaseClient
        : APP.Utility.HttpClientUtils.IClientStrategy
    {
        private Encoding _charset;
        private string _format;
        private string _contenttype;

        public int Timeout { get; protected set; } = 15_000;

        /// <summary>
        /// 内容编码
        /// 默认UTF-8
        /// </summary>
        public Encoding Charset
        {
            get { return this._charset ?? Encoding.UTF8; }
            set { if (null != value) this._charset = value; }
        }

        /// <summary>
        /// 参数格式
        /// 默认hash
        /// </summary>
        public string Format
        {
            get { return this._format.ValueOrEmpty("query_string"); }
            set { this._format = value.ValueOrEmpty("query_string"); }
        }

        public string ContentType
        {
            get { return this._contenttype.ValueOrEmpty("application/x-www-form-urlencoded"); }
            set { this._contenttype = value.ValueOrEmpty("application/x-www-form-urlencoded"); }
        }

        /// <summary>
        /// HttpClient实例
        /// </summary>
        protected HttpClient Client { get; set; }


        public virtual string Get(string url, int? timeout = null)
        {
            var tokenSource = new CancellationTokenSource();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                this.Verify(url);
                this.SetServicePoint(url);

                var result = default(string);

                var task = this.Client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                if (task.Wait(millisecondsTimeout: timeout ?? this.Timeout, cancellationToken: tokenSource.Token))
                {
                    var response = task.Result;
                    //if (response.IsSuccessStatusCode)
                    //    result = this.Charset.GetString(response.Content.ReadAsByteArrayAsync().Result);
                    //else
                    //    LogHelper.Error($"BaseClient.Get【{url}】，【{watch.ElapsedMilliseconds} ms】，【StatusCode】：【{response.StatusCode}】，【ReasonPhrase】：【{response.ReasonPhrase}】");
                    result = this.GetResultString(response);
                    //if (result == null)
                    //    LogHelper.Error($"BaseClient.Get【{url}】，【{watch.ElapsedMilliseconds} ms】，【StatusCode】：【{response.StatusCode}】，【ReasonPhrase】：【{response.ReasonPhrase}】");
                }
                else
                {
                    tokenSource.Cancel();
                    throw new OperationCanceledException("请求超时，系统已自动断开链接");
                }

                return result.ValueOrEmpty();
            }
            #region catch + finally

            catch (AggregateException ex)
            {
                var list_err_msg = new List<string>();
                ex.Handle(innerException =>
                {
                    list_err_msg.Add(innerException?.InnerException?.Message.ValueOrEmpty(innerException?.Message));
                    return true;
                });

                //throw new AggregateException(list_err_msg.Join());
                throw new Exception(list_err_msg.Join());   // 转为普通Exception
            }
            //catch (System.Threading.ThreadAbortException ex)
            //{
            //    LogHelper.Error(JSON.Serialize(new { Url = url, ThreadAbortException = ex }));
            //    System.Threading.Thread.ResetAbort();
            //    throw ex;
            //}
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                watch.Stop();
                tokenSource.Dispose();
            }

            #endregion catch + finally
        }

        public virtual string Post(string url, object content, object header = null, int? timeout = null)
        {
            var str_content = default(string);
            var tokenSource = new CancellationTokenSource();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                this.Verify(url);
                this.SetServicePoint(url);

                var result = default(string);
                str_content = this.GetContentString(content);
                using (HttpContent requestContent = new StringContent(str_content, this.Charset, this.ContentType))
                {
                    this.SetContentHeader(requestContent, header);

                    var task = this.Client.PostAsync(url, requestContent);
                    if (task.Wait(millisecondsTimeout: timeout ?? this.Timeout, cancellationToken: tokenSource.Token))
                    {
                        var response = task.Result;
                        //if (response.IsSuccessStatusCode)
                        //    result = this.Charset.GetString(response.Content.ReadAsByteArrayAsync().Result);
                        //else
                        //    LogHelper.Error($"BaseClient.Post【{url}】，【{watch.ElapsedMilliseconds} ms】，【StatusCode】：【{response.StatusCode}】，【ReasonPhrase】：【{response.ReasonPhrase}】");
                        result = this.GetResultString(response);
                        //if (result == null)
                        //    LogHelper.Error($"BaseClient.Post【{url}】，【{watch.ElapsedMilliseconds} ms】，【StatusCode】：【{response.StatusCode}】，【ReasonPhrase】：【{response.ReasonPhrase}】");

                        requestContent.Dispose();
                    }
                    else
                    {
                        requestContent.Dispose();
                        tokenSource.Cancel();
                        throw new OperationCanceledException("请求超时，系统已自动断开链接");
                    }
                }

                return result.ValueOrEmpty();
            }
            #region catch + finally

            catch (AggregateException ex)
            {
                var list_err_msg = new List<string>();
                ex.Handle(innerException =>
                {
                    list_err_msg.Add(innerException.Message);
                    return true;
                });

                //throw new AggregateException(list_err_msg.Join());
                throw new Exception(list_err_msg.Join());   // 转为普通Exception
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                watch.Stop();
                tokenSource.Dispose();
            }

            #endregion catch + finally
        }

        public virtual string Post(string url, object content, IDictionary<string, FileItem> fileParams, object header = null, int? timeout = null)
        {
            if (fileParams == null || fileParams.Count == 0)
                return Post(url, content, header, timeout);

            var tokenSource = new CancellationTokenSource();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                this.Verify(url);
                this.SetServicePoint(url);

                var result = default(string);
                using (var requestContent = new MultipartFormDataContent())
                {
                    this.SetContentHeader(requestContent, header);

                    #region 添加文件

                    IEnumerator<KeyValuePair<string, FileItem>> fileEnum = fileParams.GetEnumerator();
                    while (fileEnum.MoveNext())
                    {
                        var key = fileEnum.Current.Key;
                        var fileItem = fileEnum.Current.Value;

                        //StreamContent streamConent = new StreamContent(fileItem.GetStream());
                        //ByteArrayContent imageContent = new ByteArrayContent(streamConent.ReadAsByteArrayAsync().Result);
                        //imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                        //httpContent.Add(imageContent, key, fileItem.GetFileName());

                        var s_content = new StreamContent(fileItem.GetStream());
                        requestContent.Add(s_content, key, fileItem.GetFileName());
                        //requestContent.Add(s_content, key, $"\"{fileItem.GetFileName()}\"");
                    }

                    #endregion 添加文件

                    #region 添加参数

                    if (content == null)
                    {// 无参数
                    }
                    else if (content is IDictionary)
                    {
                        var param = content as IDictionary;
                        foreach (string key in param.Keys)
                            requestContent.Add(new StringContent(param[key].ToString()), key);
                    }
                    else if (content is string)
                    {
                        //httpContent.Add(new StringContent(Convert.ToString(content), this.Charset, this.ContentType));
                        requestContent.Add(new StringContent(Convert.ToString(content)));
                    }
                    else
                    {
                        var str_content = this.GetContentString(content);
                        requestContent.Add(new StringContent(str_content, this.Charset));
                    }

                    #endregion 添加参数

                    var task = this.Client.PostAsync(url, requestContent);
                    if (task.Wait(millisecondsTimeout: timeout ?? this.Timeout, cancellationToken: tokenSource.Token))
                    {
                        var response = task.Result;
                        //if (response.IsSuccessStatusCode)
                        //    result = this.Charset.GetString(response.Content.ReadAsByteArrayAsync().Result);
                        //else
                        //    LogHelper.Error($"BaseClient.Post【{url}】，【{watch.ElapsedMilliseconds} ms】，【StatusCode】：【{response.StatusCode}】，【ReasonPhrase】：【{response.ReasonPhrase}】");
                        result = this.GetResultString(response);
                        //if (result == null)
                        //    LogHelper.Error($"BaseClient.Post【{url}】，【{watch.ElapsedMilliseconds} ms】，【StatusCode】：【{response.StatusCode}】，【ReasonPhrase】：【{response.ReasonPhrase}】");

                        requestContent.Dispose();
                    }
                    else
                    {
                        requestContent.Dispose();
                        tokenSource.Cancel();
                        throw new OperationCanceledException("请求超时，系统已自动断开链接");
                    }
                }

                return result.ValueOrEmpty();
            }
            #region catch + finally

            catch (AggregateException ex)
            {
                var list_err_msg = new List<string>();
                ex.Handle(innerException =>
                {
                    list_err_msg.Add(innerException.Message);
                    return true;
                });

                //throw new AggregateException(list_err_msg.Join());
                throw new Exception(list_err_msg.Join());   // 转为普通Exception
            }
            catch (ThreadAbortException ex)
            {
                Thread.ResetAbort();
                throw ex;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                watch.Stop();
                tokenSource.Dispose();
            }

            #endregion catch + finally
        }

        /// <summary>
        /// 异步POST
        /// 只发不收
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public virtual async void PostNoResponse(string url, object content, object header = null, int? timeout = null)
        {
            try
            {
                this.Verify(url);
                this.SetServicePoint(url);

                var str_content = this.GetContentString(content);
                HttpContent httpContent = new StringContent(str_content, this.Charset, this.ContentType);

                this.SetContentHeader(httpContent, header);

                await Task.Run(() =>
                {
                    var isCompleted = this.Client.PostAsync(url, httpContent)
                    .ContinueWith(requestTask =>
                    {
                        Task<HttpResponseMessage> response = requestTask;
                        if (response.Status == TaskStatus.RanToCompletion)
                        {
                            HttpResponseMessage result = response.Result;
                            result.EnsureSuccessStatusCode();
                            result.Content.ReadAsStringAsync().ContinueWith(readTask =>
                            {
                                //LogHelper.Debug(string.Format("BaseClient.PostAsync：{0}", readTask.Result));
                            });
                        }
                    }).Wait(millisecondsTimeout: 20);
                });
            }
            catch (AggregateException ex)
            {
                var list_err_msg = new List<string>();
                ex.Handle(innerException =>
                {
                    list_err_msg.Add(innerException.Message);
                    return true;
                });

                //throw new AggregateException(list_err_msg.Join());
                throw new Exception(list_err_msg.Join());   // 转为普通Exception
            }
            catch (ThreadAbortException ex)
            {
                Thread.ResetAbort();
                throw ex;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<string> PostAsync(string url, object content, object header = null, int? timeout = null) => throw new NotImplementedException();

        public Task<string> GetAsync(string url, int? timeout = null) => throw new NotImplementedException();


        protected virtual void SetContentHeader(HttpContent httpContent, dynamic header)
        {
            if (httpContent is HttpContent
                && header is IDictionary)
            {
                var _header = header as IDictionary;
                foreach (string k in _header.Keys)
                {
                    var v = _header[k].ValueOrEmpty();
                    if (!v.IsNullOrWhiteSpace())
                        httpContent.Headers.TryAddWithoutValidation(k, v);
                }
            }
        }

        private string GetContentString(object content)
        {
            var _content = string.Empty;

            if (content is string)
                return Convert.ToString(content);

            if (content is IDictionary dic)
            {
                switch (this.Format)
                {
                    case "xml":
                        _content = WebCommon.BuildXml(dic);
                        break;

                    //case "json":
                    //    _content = JSON.Serialize(dic);
                    //    break;

                    case "query_string":
                    case "hash":
                    default:
                        _content = WebCommon.BuildQueryString(dic, false);
                        break;
                }
            }

            return _content;
        }

        /// <summary>
        /// 获取响应报文
        /// 【部分通道响应StatusCode状态码非200，也能取到报文，需各自override相应规则】
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected virtual string GetResultString(HttpResponseMessage response)
        {
            string result = null;

            if (response.IsSuccessStatusCode)
                result = this.Charset.GetString(response.Content.ReadAsByteArrayAsync().Result);

            return result;
        }

        private void Verify(string url)
        {
            if (Client == null)
                throw new Exception("连接对象为空");

            if (string.IsNullOrWhiteSpace(url))
                throw new Exception("请求地址为空");
        }

        private void SetServicePoint(string url)
        {
            var uri = new Uri(url);
            System.Net.ServicePointManager.Expect100Continue = false;
            var servicePoint = System.Net.ServicePointManager.FindServicePoint(new Uri(url));
            if (servicePoint != null)
                servicePoint.ConnectionLeaseTimeout = 1000 * 60 * 5;

            if ("https".Same(uri.Scheme))
            {
                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                System.Net.ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }
        }

        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => true;
    }
}
