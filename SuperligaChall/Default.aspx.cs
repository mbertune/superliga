using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Data.SqlClient;


namespace SuperligaChall
{
    public partial class Default : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable CSVTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        
    
        protected void btn_import_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    int flag = 0;
                    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string RandomName = DateTime.Now.ToFileTime().ToString();
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string FolderPath = "~/upload/";

                    string FilePath = Server.MapPath(FolderPath + RandomName + FileName);

                    string[] filenames = Directory.GetFiles(Server.MapPath("~/upload"));

                    if (filenames.Length > 0)
                    {
                        foreach (string filename in filenames)
                        {
                            if (FilePath == filename)
                            {

                                flag = 1;
                                break;
                            }
                        }

                        if (flag == 0)
                        {
                            FileUpload1.SaveAs(FilePath);
                            ReadCSVFile(FilePath);
                        }

                    }
                    else
                    {
                        FileUpload1.SaveAs(FilePath);
                        ReadCSVFile(FilePath);
                    }
                }
                else
                {
                    String msg = "Debe elegir un archivo";
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public void ReadCSVFile(string fileName)
        {
            try
            {
              

                CSVTable.Columns.Add("Nombre", typeof(string));
                CSVTable.Columns.Add("Edad", typeof(int));
                CSVTable.Columns.Add("Club", typeof(string));
                CSVTable.Columns.Add("EstadoCivil", typeof(string));
                CSVTable.Columns.Add("Estudios", typeof(string));

                if (File.Exists(fileName) && new FileInfo(fileName).Length > 0)
                {
                    try
                    {

                        string[] lineas = File.ReadAllLines(fileName);


                        foreach(var linea in lineas)
                        {
                            var valores = linea.Split(';');
                            CSVTable.Rows.Add(valores[0],valores[1], valores[2], valores[3], valores[4]);
                        }

                        if ((CSVTable != null) && (CSVTable.Rows.Count > 0))
                        {
                             


                            

                            var promedio = CSVTable.AsEnumerable().Where(x => x.Field<string>("Club") == "Racing").
                                Select(x => x.Field<int>("Edad")).Average();

                            var lista100 = (from fila in CSVTable.AsEnumerable()
                                           where fila.Field<string>("Estudios") == "Universitario" && fila.Field<string>("EstadoCivil") == "Casado"
                                           orderby fila.Field<int>("Edad")
                                           select new { nombre = fila.Field<string>("Nombre"), 
                                               edad = fila.Field<int>("Edad"),
                                               club = fila.Field<string>("Club")
                                           }).Take(100);

                            var nombresRiver = from fila in CSVTable.AsEnumerable()
                                           where fila.Field<string>("Club") == "River"
                                               select new
                                               {
                                                   nombre = fila.Field<string>("Nombre"),
                                                   club = fila.Field<string>("club"),
                                               };

                            var comunRiver = nombresRiver.GroupBy(n => n.nombre).
                                Select(g => new { nombre = g.Key, Cantidad = g.Count() })
                                .OrderBy(x => x.Cantidad).Take(5);

                            var sociosXClub = CSVTable.AsEnumerable().
                                              GroupBy(c => c.Field<string>("Club")).
                                              Select(g => new {
                                                 club=  g.Key, socios =g.Count()
                                              });

                            var promedioEdadXClub = CSVTable.AsEnumerable().
                                              GroupBy(t => new { club = t.Field<string>("Club")  }).
                                              Select(g => new {
                                                  club = g.Key.club,
                                                  promedioEdad = g.Average(p=>p.Field<int>("Edad"))
                                                  

                                              });

                            var minEdadXClub = CSVTable.AsEnumerable().
                                             GroupBy(t => new { club = t.Field<string>("Club") }).
                                             Select(g => new {
                                                 club = g.Key.club,
                                                 menor = g.Min(m => m.Field<int>("Edad"))

                                             });

                            var maxEdadXClub = CSVTable.AsEnumerable().
                                           GroupBy(t => new { club = t.Field<string>("Club") }).
                                           Select(g => new {
                                               club = g.Key.club,
                                               mayor = g.Max(m => m.Field<int>("Edad"))

                                           });

                            var lista = from p in promedioEdadXClub
                                        join min in minEdadXClub on p.club equals min.club
                                        join max in maxEdadXClub on min.club equals max.club
                                        select new
                                        {
                                            club = p.club,
                                            edadProm = p.promedioEdad,
                                            edadMin = min.menor,
                                            edadMax = max.mayor
                                        };

                            

                            lblCantidad.Text = "E1 - Cantidad total de socios: " + CSVTable.Rows.Count.ToString();

                            lblPromedio.Text = "E2 - Promedio de edad socios Racing: " + Math.Round(promedio, 2).ToString();

                            gvd100.DataSource = lista100;
                            
                            gvd100.DataBind();

                            river.DataSource = comunRiver;
                            river.DataBind();

                            EdadPromedio.DataSource = lista;
                            EdadPromedio.DataBind();

                            //var lista100 = CSVTable.AsEnumerable().
                            //    Where(x => x.Field<string>("Estudios") == "Universitario").
                            //    Select new { a = x.Field<string>("Nombre"), b = x.Field<string>("Estudios") });


                            //txtCantidad.Text = CSVTable.Rows.Count.ToString();
                        }
                        else
                        {
                            String msg = "No hay info";
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(String.Format("Error reading Table {0}.\n{1}", Path.GetFileName(fileName), ex.Message));
                    }
                }
            }
            catch (Exception ex) {
            }
        }
    }
}