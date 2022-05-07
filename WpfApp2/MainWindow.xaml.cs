using System;
using System.Text;
using System.Windows;
using System.IO;
using System.Net;

namespace VRChatSearcherBeta

{
    public partial class MainWindow : Window

    {
        private void SubmitData()
        {
            try
            {
                string serverkey = ""; //enter Avatar Data Server
                string user = TextBox_UserID.Text;
                string pass = Text_Password.Text;
                string postData = "user_id=" + user + "&pin=" + pass;
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(postData);
                CookieContainer PHPSESSID = new CookieContainer();

                string url = "https://" + serverkey + "/api/login.php";
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.41 Safari/537.36";
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.CookieContainer = new CookieContainer();
                Stream stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                stream = response.GetResponseStream();
                PHPSESSID = request.CookieContainer;

                StreamReader sr = new StreamReader(stream);
                //MessageBox.Show(sr.ReadToEnd());
                string html = sr.ReadToEnd();
                //Textbox_Log.Text = html;
                sr.Close();
                stream.Close();


                string url2 = "https://" + serverkey + "/api/search.php?searchTerm=" + Textbox_URL.Text;
                HttpWebRequest request2 = WebRequest.CreateHttp(url2);
                request2.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.41 Safari/537.36";
                request2.Method = "GET";
                request2.CookieContainer = PHPSESSID;

                response = (HttpWebResponse)request2.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                html = streamReader.ReadToEnd();
                streamReader.Close();
                Textbox_Log.Text = html;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            SubmitData();
        }

        private void Textbox_URL_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void Textbox_Log_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}