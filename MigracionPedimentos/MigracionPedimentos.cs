using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracionPedimentos
{
    public class MigracionPedimentos
    {

        public static void Pedimentos(string path)
        {
            List<GetEquiposExportacionSinCT_Result> listPedimentos;
            string anio, codigoDes, numeroPed, clavePed, remesa, tipoPed;
            string[] arrayPed;
            string[] arrayNP;
            int indexC;
            Imex_Info_EntregaAduana_Pedimentos insertPedimento;
            Imex_Info_EntregaAduana dataAd;

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, "pedimentosMigrados.txt"), true))
            {
                try
                {
                    outputFile.WriteLine("Inicia la migración de pedimentos");

                    using (MembershipEntities ctx = new MembershipEntities())
                    {

                        listPedimentos = ctx.GetEquiposExportacionSinCT().ToList();
                        outputFile.WriteLine($"Se tienen {listPedimentos.Count} registros a migrar");

                        foreach (GetEquiposExportacionSinCT_Result currPed in listPedimentos)
                        {
                            try
                            {
                                insertPedimento = new Imex_Info_EntregaAduana_Pedimentos();
                                anio = string.Empty;
                                codigoDes = string.Empty;
                                numeroPed = string.Empty;
                                clavePed = string.Empty;
                                remesa = string.Empty;
                                tipoPed = string.Empty;

                                if (currPed.Pedimento != null && currPed.Pedimento.Length > 0)
                                {
                                    arrayPed = currPed.Pedimento.Split(' ');
                                    tipoPed = "E";

                                    //24 84 0973 4000480-7
                                    //245234384012063 / 115
                                    if (arrayPed.Length > 1)
                                    {
                                        anio = arrayPed[0];
                                        codigoDes = arrayPed[1];
                                        clavePed = arrayPed[2].TrimStart('0');
                                        if (arrayPed[3].Contains("-"))
                                        {
                                            arrayNP = arrayPed[3].Split('-');
                                            numeroPed = arrayNP[0];
                                            remesa = arrayNP[1];
                                        }
                                        else if (arrayPed[3].Contains("/"))
                                        {
                                            arrayNP = arrayPed[3].Split('/');
                                            numeroPed = arrayNP[0];
                                            remesa = arrayNP[1];
                                        }
                                        else
                                        {
                                            numeroPed = arrayPed[3];
                                        }
                                    }
                                    else
                                    {
                                        //245232024007180
                                        //24523800401441 / 35                                
                                        //245234384012063 / 115
                                        anio = currPed.Pedimento.Substring(0, 2);
                                        var terminalAduana = ctx.Imex_TerminalAduana.FirstOrDefault(ta => ta.TerminalId == currPed.TerminalId);
                                        if (terminalAduana != null)
                                        {
                                            codigoDes = terminalAduana.ClaveRecinto;
                                        }
                                        clavePed = currPed.Pedimento.Substring(4, 3);
                                        if (currPed.Pedimento.Contains("/"))
                                        {
                                            indexC = currPed.Pedimento.IndexOf('/');
                                            numeroPed = currPed.Pedimento.Substring(7, currPed.Pedimento.Length - indexC );
                                            remesa = currPed.Pedimento.Substring(indexC, currPed.Pedimento.Length - indexC);
                                        }
                                        else
                                        {
                                            numeroPed = currPed.Pedimento.Substring(7, currPed.Pedimento.Length - 7);
                                        }
                                    }

                                    insertPedimento = new Imex_Info_EntregaAduana_Pedimentos()
                                    {
                                        InfoEntregaId = currPed.InfoEntregaId.Value,
                                        Anio = anio.Length > 2 ? anio.Substring(0, 2) : anio,
                                        CodigoDespacho = codigoDes.Length > 2 ? codigoDes.Substring(0, 2) : codigoDes,
                                        NumeroPedimento = numeroPed,
                                        ClavePedimento = clavePed.Length > 3 ? clavePed.Substring(0, 3) : clavePed,
                                        Remesa = remesa.Length > 15 ? remesa.Substring(0, 15) : remesa,
                                        TipoPedimento = tipoPed,
                                        ProcesadoTMS = true,
                                        FechaProcesadoTMS = DateTime.Now,
                                        Activo = true,
                                        FechaModificacion = DateTime.Now,

                                    };

                                    dataAd = ctx.Imex_Info_EntregaAduana.Where(a => a.InfoEntregaId == currPed.InfoEntregaId).FirstOrDefault();
                                    ctx.Imex_Info_EntregaAduana_Pedimentos.Add(insertPedimento);                                    
                                    ctx.SaveChanges();

                                }
                                else
                                {
                                    outputFile.WriteLine($"{currPed.Container}, no se tiene un pedimento capturado");
                                }
                            }
                            catch (Exception ex)
                            {
                                dataAd = ctx.Imex_Info_EntregaAduana.Where(a => a.InfoEntregaId == currPed.InfoEntregaId).FirstOrDefault();
                                dataAd.Pedimento = string.Empty;
                                ctx.SaveChanges();

                                outputFile.WriteLine($"{currPed.Container}, no se pudo migrar el pedimento ({currPed.Pedimento}), error: {ex.Message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    outputFile.WriteLine($"Ha ocurrido un error: {ex.Message}");
                }
                
            }
        }

    }
}
