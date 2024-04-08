using MvcCoreApiClient.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MvcCoreApiClient.Services
{
    public class ServiceHospital
    {
        //CLASE PARA INDICAR EL FORMATO QUE ESTAMOS 
        //CONSUMIENDO
        private MediaTypeWithQualityHeaderValue header;

        //NUESTRA URL DEL SERVICIO
        private string ApiUrl;

        public ServiceHospital(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>("ApiUrls:ApiHospitales");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<Hospital> FindHospitalAsync(int idHospital)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/hospitales/" + idHospital;
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if(response.IsSuccessStatusCode)
                {
                    Hospital data = await response.Content.ReadAsAsync<Hospital>();
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }

        //CREAMOS UN METODO ASINCRONO PARA LEER LOS HOSPITALES
        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            //UTILIZAMOS LA CLASE HttpClient PARA LAS PETICIONES
            using (HttpClient client = new HttpClient())
            {
                //NECESITAMOS LA PETICION
                string request = "api/hospitales";
                //INDICAMOS LA URL BASE DE NUESTRO SERVICIO
                client.BaseAddress = new Uri(this.ApiUrl);
                //COMO NORMA, DEBEMOS LIMPIAR LAS CABECERAS EN 
                //CADA PETICION, POR SI EN ALGUN MOMENTO LAS MEZCLAMOS 
                //Y NOS DARIA ERROR.
                client.DefaultRequestHeaders.Clear();
                //INDICAMOS EL TIPO DE CONSULTA QUE VAMOS A CONSUMIR
                client.DefaultRequestHeaders.Accept.Add
                    (this.header);
                //REALIZAMOS LA PETICION Y ALMACENAMOS LOS 
                //RESULTADOS EN UNA RESPUESTA
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    //PRIMERO VAMOS A REALIZAR LA PETICION
                    //UTILIZANDO NewtonSoft, PARA QUE TENGAIS 
                    //UN EJEMPLO SI LAS PROPIEDADES DEL JSON
                    //Y DEL MODEL NO FUERAN IGUALES
                    string json =
                        await response.Content.ReadAsStringAsync();
                    List<Hospital> data = JsonConvert.DeserializeObject<List<Hospital>>(json);
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
