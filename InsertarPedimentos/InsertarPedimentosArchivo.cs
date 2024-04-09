using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertarPedimentos
{
    public class InsertarPedimentosArchivo
    {

        public static void LeerInsertaPedimentos(string currPath)
        {
            string sep = "\t";
            string fullPath = $"{currPath}\\PedimentosInsertVF.txt";
            string contenedor, anio, codigoDes, numeroPed, clavePed, remesa, tipoPed;
            string[] arrayLine;
            Imex_Info_EntregaAduana_Pedimentos insertPedimento;
            Imex_Container container;
            GridInventarioV20_Result contInv;
            Imex_Info_EntregaAduana contAduana;
            string dataPed;

            try
            {
                if (File.Exists(Path.Combine(currPath, "ResultadoInsertarPedimentos.txt")))
                {
                    File.Delete(Path.Combine(currPath, "ResultadoInsertarPedimentos.txt"));
                }

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(currPath, "ResultadoInsertarPedimentos.txt"), true))
                {                    
                    try
                    {
                        outputFile.WriteLine($"Inicia el proceso de inserción");
                        IEnumerable<string> lines = File.ReadLines(fullPath);                        

                        using (MembershipEntities ctx = new MembershipEntities())
                        {
                            outputFile.WriteLine($"Limpiando el pedimento en estructura anterior");
                            ctx.UpdatePedimentoExportacion();
                            outputFile.WriteLine($"Ya se limpiaron los pedimentos");

                            outputFile.WriteLine($"Se tienen {lines.Count()} lineas en el archivo");

                            foreach (string currLine in lines)
                            {
                                insertPedimento = new Imex_Info_EntregaAduana_Pedimentos();
                                contenedor = string.Empty;
                                anio = string.Empty;
                                codigoDes = string.Empty;
                                numeroPed = string.Empty;
                                clavePed = string.Empty;
                                remesa = string.Empty;
                                tipoPed = string.Empty;
                                dataPed = string.Empty;

                                if (currLine.Trim().Length > 0)
                                {
                                    arrayLine = currLine.Split(sep.ToCharArray());

                                    if (arrayLine.Length >= 5)
                                    {
                                        tipoPed = "E";
                                        contenedor = arrayLine[0];
                                        anio = arrayLine[1];
                                        codigoDes = arrayLine[2];
                                        clavePed = arrayLine[5];
                                        numeroPed = arrayLine[3];
                                        remesa = arrayLine.Length > 5 ? arrayLine[4] : string.Empty;                                        

                                        container = ctx.Imex_Container.Where(c => c.Container == contenedor.Trim()).FirstOrDefault();

                                        if (container != null)
                                        {
                                            contInv = ctx.GridInventarioV20("etorres", 0, 0, 0, 0, 2, 1, 6, 1, 0, 0, contenedor.Trim(), 1, string.Empty, 0, 0, 0, 0, 0, 0, 0).FirstOrDefault();

                                            if (contInv != null)
                                            {
                                                contAduana = ctx.Imex_Info_EntregaAduana.Where(a=> a.ContainerId == container.ContainerId && a.TerminalId == contInv.TerminalId).OrderByDescending(x => x.InfoEntregaId).FirstOrDefault();

                                                if (contAduana != null)
                                                {

                                                    if ((!string.IsNullOrEmpty(anio.Trim())) && (!string.IsNullOrEmpty(codigoDes.Trim())) &&
                                                        (!string.IsNullOrEmpty(numeroPed.Trim())) && (!string.IsNullOrEmpty(clavePed.Trim())))
                                                    {
                                                        insertPedimento = new Imex_Info_EntregaAduana_Pedimentos()
                                                        {
                                                            InfoEntregaId = contAduana.InfoEntregaId,
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

                                                        ctx.Imex_Info_EntregaAduana_Pedimentos.Add(insertPedimento);
                                                        ctx.SaveChanges();

                                                        List<Imex_Info_EntregaAduana_Pedimentos> pedimentosEnt = ctx.Imex_Info_EntregaAduana_Pedimentos.Where(p => p.InfoEntregaId == contAduana.InfoEntregaId && p.TipoPedimento == "E").ToList();

                                                        //dataPed = string.IsNullOrEmpty(remesa.Trim()) ? $"{anio} {codigoDes} {numeroPed}" : $"{anio} {codigoDes} {numeroPed}-{remesa}";
                                                        dataPed = string.Join("/", pedimentosEnt.Select(p => (($"{p.Anio} {p.CodigoDespacho} {p.NumeroPedimento}-{p.Remesa}".Trim()).EndsWith("-") ? ($"{p.Anio} {p.CodigoDespacho} {p.NumeroPedimento}-{p.Remesa}".Trim()).Remove(($"{p.Anio} {p.CodigoDespacho} {p.NumeroPedimento}-{p.Remesa}".Trim()).Length - 1, 1) : ($"{p.Anio} {p.CodigoDespacho} {p.NumeroPedimento}-{p.Remesa}".Trim()))));
                                                        contAduana.Pedimento = dataPed.Length > 70 ? dataPed.Substring(0, 69) : dataPed;
                                                        contAduana.ClavePedimento = pedimentosEnt.FirstOrDefault() != null ? pedimentosEnt.FirstOrDefault().ClavePedimento.Length > 70 ? pedimentosEnt.FirstOrDefault().ClavePedimento.Substring(0, 69) : pedimentosEnt.FirstOrDefault().ClavePedimento : null;

                                                        ctx.SaveChanges();

                                                        outputFile.WriteLine($"Para el contenedor {contenedor.Trim()},se inserto un pedimento ({dataPed})");
                                                    }
                                                    else
                                                    {
                                                        outputFile.WriteLine($"Para el contenedor {contenedor.Trim()}, al insertar un pedimento los datos: Año {anio}, Codigo Despacho {codigoDes}, Numero Pedimento {numeroPed}, Clave Pedimento {clavePed}, son obligatorios");
                                                    }

                                                   
                                                }
                                                else
                                                {
                                                    outputFile.WriteLine($"Para el contenedor {contenedor.Trim()}, no se encontro registro en la aduana");
                                                }
                                            }
                                            else
                                            {
                                                outputFile.WriteLine($"El contenedor {contenedor.Trim()} no existe en el inventario, en el patio Lleno Exportación Fiscalizado");
                                            }
                                        }
                                        else
                                        {
                                            outputFile.WriteLine($"El contenedor {contenedor.Trim()} no existe");
                                        }
                                    }
                                    else
                                    {
                                        outputFile.WriteLine($"Esta linea ({currLine}) no tiene el formato correcto");
                                    }

                                }                                
                            }
                        }   
                    }
                    catch (Exception ex)
                    { 
                        outputFile.WriteLine($"Ocurrio un error: {ex.Message}");
                    }                    
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un error: {ex.Message}");
            }
        }

    }
}
