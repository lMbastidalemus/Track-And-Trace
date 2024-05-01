using DL;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Paquete
    {
        public static (bool, string, ML.Paquete?, Exception?) GetAll()
        {
			ML.Paquete paquete = new ML.Paquete();
			try
			{
				using (DL.JoaxacaTrackTraceContext context = new DL.JoaxacaTrackTraceContext())
				{
					var query = (from Paquete in context.Paquetes
								 select new
								 {
                                     IdPaquete = Paquete.IdPaquete,
                                     InstruccionEntrega = Paquete.InstruccionEntrega,
									 Peso = Paquete.Peso,
								     DireccionOrigen = Paquete.DireccionOrigen,
									 DireccionEntrega = Paquete.DireccionEntrega,
									 FechaEstimadaEntrega = Paquete.FechaEstimadaEntrega,
								     NumeroGuia = Paquete.NumeroGuia
								  }).ToList();

					if (query.Count > 0)
					{
						paquete.Paquetes = new List<ML.Paquete>();

                        foreach (var item in query)
                        {
							ML.Paquete objPaquete = new ML.Paquete();

							objPaquete.IdPaquete = item.IdPaquete;
							objPaquete.InstruccionEntrega = item.InstruccionEntrega;
							objPaquete.Peso = item.Peso;
							objPaquete.DireccionOrigen = item.DireccionOrigen;
							objPaquete.DireccionEntrega = item.DireccionEntrega;
							objPaquete.FechaEstimadaEntrega = item.FechaEstimadaEntrega;
							objPaquete.NumeroGuia = item.NumeroGuia;

							paquete.Paquetes.Add(objPaquete);
                        }
						return (true, "Se encontro los datos", paquete, null);
                    }
					else
					{
                        return (false, "No tienes algun un registro disponible", null, null);
                    }
				}
			}
			catch (Exception ex)
			{

				return (false, ex.Message, null, ex);
			}
        }

		public static (bool, string?, Exception?) Add(ML.Paquete paquete)
		{
			try
			{
				using (DL.JoaxacaTrackTraceContext context = new DL.JoaxacaTrackTraceContext())
				{

                    DL.Paquete paqueteDl = new DL.Paquete();

                    paqueteDl.InstruccionEntrega = paquete.InstruccionEntrega;
                    paqueteDl.Peso = paquete.Peso;
                    paqueteDl.DireccionOrigen = paquete.DireccionOrigen;
                    paqueteDl.DireccionEntrega = paquete.DireccionEntrega;
                    paqueteDl.FechaEstimadaEntrega = paquete.FechaEstimadaEntrega;
                    paqueteDl.NumeroGuia = paquete.NumeroGuia;

					context.Paquetes.Add(paqueteDl);

                    int rowAffected = context.SaveChanges();

					if (rowAffected > 0)
					{
						return (true, "Validado el Paquete", null);
					}
					else
					{
						return (false, "Algo salio mal", null);
					}
                }

            }
			catch (Exception ex)
			{

				return (false, ex.Message, ex);
			}
		}


    }
}
