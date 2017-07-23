<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
       <style>
    body {
        position: relative; 
    }

   #mystuff {
        position: absolute;
        z-index: 10;
        border-color: red;
        background-color:aquamarine;
        width: 100%;
    }

   #theirstuff {
        position: relative;
        z-index: 1;
        width:100%;
        align-content:center;
    }
   </style>
    <script src = 'https://code.jquery.com/jquery-3.1.1.min.js'></script> 
   <script type="text/javascript" src="jquery.facedetection.min.js"></script>
    <title>Web</title>
</head>
<body>
    <form id="form1" runat="server">
         <div id="mystuff"  >
         Browse the Web:<asp:TextBox ID="txtUrl" runat="server" Width="300"/>
        <asp:Button ID="btnSubmit" runat="server" Text="Get the Webpage" Width="120px" OnClick="Gethtml" />  
        <!-- <a id ='try-it' href ='#'>-->
             <button class="mybuttons" id ='try-it'>Blur faces</button>
        <!-- </a>-->
        <br />
        <i>Enter a URL starting with <code>http://</code></i><br />  
    </div> 
    </form>
     <div id="theirstuff">
             <asp:Label ID="lblHTML" runat="server" Width="100%" ></asp:Label>            
        </div>  
</body>
</html>
