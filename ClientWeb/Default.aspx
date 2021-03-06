﻿<%@ page title="" language="C#" masterpagefile="~/General.Master" autoeventwireup="true" codebehind="Default.aspx.cs" inherits="ClientWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:content id="Content2" contentplaceholderid="main" runat="server">


	<figure class="fullscreen">
		<h1>L'incroyable agence immobilière !</h1>
		<div id="searchbox">
			<a href="/Catalogue.aspx">Trouver le bien de votre rêve</a>
			<!--<form action="Catalogue.aspx" EnableViewState="false" runat="server">
				
				<asp:TextBox ID="RechercheTitre_simple" placeholder="Quel est votre rêve ?" class="" AutoPostBack="false" runat="server"  />
				<asp:Button Text="&#x1f50d;" runat="server" />
				
				
				<input type="hidden" id="searchtype" checked name="searchtype" value="simple" />
			</form>-->
		</div>
	</figure>
		

	<div id="main">
		<div class="container wrap">
			
			<h2>Les derniers biens</h2>
			
			<div class="row">


				<asp:Repeater ID="rpResultats" runat="server">
					<ItemTemplate>

						<div class="tilecolumn">
					
							<a href=<%# "/Bien.aspx?id="+Eval("Id").ToString()%> class="tile" style="<%# Eval("PhotoPrincipaleBase64").ToString().CompareTo("") == 0 ? "background-size: contain;" : ("background-image:url(data:image/png;base64,"+ Eval("PhotoPrincipaleBase64")+ ")") %>">

								<div class="informations">
									<div class="title">
										<div class="onerow"><span><%# Eval("Titre") %></span></div>
									</div>
									<div class="clear">
										<div class="onerow"><span>&#x1F3E0; <%# Eval("CodePostal") %>, <%# Eval("Ville") %></span></div>
									</div>
									<div class="clear">
										<div class="onerow highlight"><span><%# ((double)Eval("Prix")==0) ? "Pas de prix" : Eval("Prix").ToString() + " €" %></span></div>
									</div>
									<div class="clear">
										<div class="onerow center"><span>Cliquer pour en voir plus</span></div>
									</div>
									<!--<div class="clear">
										<div class="tworow">65 m²</div>
										<div class="tworow"><%# ((double)Eval("Prix")==0) ? "Pas de prix" : Eval("Prix").ToString() %></div>
									</div>
									<div class="clear">
										<div class="on">
											<div class="tworow">5<sup>ème</sup> étage</div>
											<div class="tworow">6 pièces<sup>&nbsp;</sup></div>
										</div>
										<div class="off">
											<div class="center onerow"><span>Cliquer pour en voir plus<sup>&nbsp;</sup></span></div>
										</div>
									</div>
								-->
								</div>
							</a>
					
						</div>

					</ItemTemplate>
					<SeparatorTemplate>
					</SeparatorTemplate>
				</asp:Repeater>

			</div>
				
			<div class="clear"></div>
				
			<div id="more">
				<a href="Catalogue.aspx">Tout notre catalogue</a>
			</div>
				
		</div>
			

	</div>

</asp:content>
