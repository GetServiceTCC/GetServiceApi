using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace GetServiceApi.Controllers
{
    [Authorize]
    public class UsuarioFotoController : ApiController
    {
        [AllowAnonymous]
        public HttpResponseMessage Get(string id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            string filePath = HostingEnvironment.MapPath("~/Fotos/" + id + ".jpg");

            if (!File.Exists(filePath))
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                Image image = Image.FromStream(fileStream);
                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, ImageFormat.Jpeg);
                result.Content = new ByteArrayContent(memoryStream.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            }

            return result;
        }
        
        public void Delete()
        {
            string userName = User.Identity.Name;

            string filePath = HostingEnvironment.MapPath("~/Fotos/" + userName + ".jpg");
            File.Delete(filePath);
        }

        public async Task<HttpResponseMessage> Post()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);

            if (Request.Content.IsMimeMultipartContent())
            {
                await Request.Content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider()).ContinueWith((task) =>
                {
                    MultipartMemoryStreamProvider provider = task.Result;
                    foreach (HttpContent content in provider.Contents)
                    {
                        Stream stream = content.ReadAsStreamAsync().Result;
                        Image image = Image.FromStream(stream);
                        var testName = content.Headers.ContentDisposition.Name;
                        string filePath = HostingEnvironment.MapPath("~/Fotos/");

                        string userName = User.Identity.Name;

                        string fileName = userName + ".jpg";
                        string fullPath = Path.Combine(filePath, fileName);
                        image.Save(fullPath);
                    }
                });

                return result;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "Esta solicitação não está formatada corretamente"));
            }
        }
    }
}