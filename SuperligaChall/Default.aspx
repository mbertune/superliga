<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SuperligaChall.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1"/>
    <title>Superliga Challenge</title>
     <link rel="stylesheet" media="screen" href="https://prod-cdn.wetransfer.net/packs/css/application-c2038279.chunk.css" />
</head>
<body style="overflow:scroll">
    
   
    <div style="overflow: auto;">
        <form id="form1b" runat="server">
             <div class="transfer">
<div class="transfer__window uploader uploader--form uploader--type-link">
<div class="scrollable transfer__contents"><div class="scrollable__content" style="margin-right: 0px;">
<div class="uploader__files"> 



<div class="uploader__empty-state uploader__empty-state--with-directories-selector uploader__empty-state--with-display-name">
<svg viewBox="0 0 72 72"><path d="M36.493 72C16.118 72 0 55.883 0 36.493 0 16.118 16.118 0 36.493 0 55.882 0 72 16.118 72 36.493 72 55.882 55.883 72 36.493 72zM34 34h-9c-.553 0-1 .452-1 1.01v1.98A1 1 0 0 0 25 38h9v9c0 .553.452 1 1.01 1h1.98A1 1 0 0 0 38 47v-9h9c.553 0 1-.452 1-1.01v-1.98A1 1 0 0 0 47 34h-9v-9c0-.553-.452-1-1.01-1h-1.98A1 1 0 0 0 34 25v9z" fill="#409fff" fill-rule="nonzero"></path></svg>
<h2>Añade tus archivos</h2>
    <button class="uploader__sub-title uploader__directories-dialog-trigger">
        <asp:FileUpload Width="500" ID="FileUpload1" runat="server" />
    </button></div>

</div>
    </div></div>
    <div class="transfer__footer">
        <asp:Button ID="btn_import" runat="server" CssClass="transfer__button transfer__button" Text="Enviar" OnClick="btn_import_Click" />
</div></div></div>
        


   <!--res-->
        <div style="width:50%;overflow: auto;">

            <div Style="position: absolute;right: 60px;">
                <div>
                    <asp:Label ID="lblCantidad" runat="server" Text=""></asp:Label>
                </div>
            <br />
                <div Style="position: absolute;right: 60px;">
                <div>
                    <asp:Label ID="lblPromedio" runat="server" Text=""></asp:Label>
                </div>
            <br />
                <div class="scrollable__content" style="margin-right: 0px;">
                     <asp:GridView ID="gvd100" runat="server" Height="210px" AutoGenerateColumns="false"
                         Caption="E3 - Las primeras 100 personas casadas, etc." style="overflow: auto;">
                        <Columns>
                            <asp:BoundField DataField="Nombre"  HeaderText="Nombre"></asp:BoundField>
                            <asp:BoundField DataField="Club"  HeaderText="Club"></asp:BoundField>
                          <asp:BoundField DataField="Edad"  HeaderText="Prom. Edad"></asp:BoundField>
                            
                        </Columns>

                    </asp:GridView>
                    </div>
                    <br />
                    <div class="scrollable__content" style="margin-right: 0px;">
                     <asp:GridView ID="river" runat="server" Height="210px" AutoGenerateColumns="false"
                         Caption="E4 - Nombres mas comunes entre hinchas de River" style="overflow: auto;">
                        <Columns>
                            <asp:BoundField DataField="Nombre"  HeaderText="Nombre"></asp:BoundField>
                          
                          <asp:BoundField DataField="Cantidad"  HeaderText="Cantidad"></asp:BoundField>
                            
                        </Columns>

                    </asp:GridView>
                    </div>
                    <br />
                <div Style="max-height=300px;overflow: auto;">
                    <asp:GridView ID="EdadPromedio" runat="server" Height="210px" AutoGenerateColumns="false"
                       Caption="E5 - Edad Promedio" >
                        <Columns>
                            <asp:BoundField DataField="Club"  HeaderText="Club"></asp:BoundField>
                          <asp:BoundField DataFormatString="{0:n}" DataField="edadProm"  HeaderText="Prom. Edad"></asp:BoundField>
                            <asp:BoundField DataField="edadMin"  HeaderText="Edad Min."></asp:BoundField>
                          <asp:BoundField DataField="edadMax"  HeaderText="Edad Max."></asp:BoundField>
                        </Columns>

                    </asp:GridView>
                    
                     
                </div>
                
            </div>
                

        </div>


        </div>
    </form>
    </div>
</body>
</html>
