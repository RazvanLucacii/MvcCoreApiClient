using MvcCoreApiClient.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcCoreApiClient.Services
{
    public class ServiceDoctor
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceDoctor(IConfiguration configuration)
        {
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiDoctores");
        }

        public async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            string request = "api/doctores";
            List<Doctor> data = await this.CallApiAsync<List<Doctor>>(request);
            return data;
        }

        public async Task<Doctor> GetDoctorAsync(int idDoctor)
        {
            string request = "api/doctores/" + idDoctor;
            Doctor data = await this.CallApiAsync<Doctor>(request);
            return data;
        }

        public async Task InsertDoctorAsync(int idHospital, int idDoctor, string apellido, string especialidad, int salario)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/doctores";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Doctor doctor = new Doctor();
                doctor.IdHospital = idHospital;
                doctor.IdDoctor = idDoctor;
                doctor.Apellido = apellido;
                doctor.Especialidad = especialidad;
                doctor.Salario = salario;
                string json = JsonConvert.SerializeObject(doctor);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            }
        }
    }
}
