using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace XamarinGoogleSheetsDB
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void SubmitButton_Pressed(System.Object sender, System.EventArgs e)
        {
            var client = new HttpClient();
            var model = new FeedbackModel()
            {
                Name = NameEntry.Text,
                Phone = PhoneEntry.Text,
                Email = EmailEntry.Text,
                Feedback = FeedbackEntry.Text
            };
            var uri = "https://script.google.com/macros/s/AKfycbzum_bRbjihAvEKnolsaT54csNtC0DMUQh0K6LIbN3HrF3L15Po/exec";
            var jsonString = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(jsonString);
            var result = await client.PostAsync(uri, requestContent);
            var resultContent = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseModel>(resultContent);
            ProcessResponse(response);
        }

        private void ProcessResponse(ResponseModel responseModel)
        {
            ResultLabel.IsVisible = true;
            ResultLabel.Text = responseModel.Message;
            if (responseModel.Status == "SUCCESS")
                ResultLabel.TextColor = Color.Black;
            else
                ResultLabel.TextColor = Color.Red;
        }
    }
}
