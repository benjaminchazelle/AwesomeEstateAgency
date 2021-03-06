﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWeb
{
	public partial class Administration : System.Web.UI.Page
	{

		private void Initcriteres(ServiceAgence.CriteresRechercheBiensImmobiliers c)
		{
			c.TitreContient = "";
			c.AdresseContient = "";
			c.CodePostal = "";
			c.DateMiseEnTransaction1 = null;
			c.DateMiseEnTransaction2 = null;
			c.DateTransaction1 = null;
			c.DateTransaction2 = null;
			c.DescriptionContient = "";
			c.EnergieChauffage = null;
			c.MontantCharges1 = -1;
			c.MontantCharges2 = -1;
			c.NbEtages1 = -1;
			c.NbEtages2 = -1;
			c.NbPieces1 = -1;
			c.NbPieces2 = -1;
			c.NumEtage1 = -1;
			c.NumEtage2 = -1;
			c.Prix1 = -1;
			c.Prix2 = -1;
			c.Surface1 = -1;
			c.Surface2 = -1;
			c.TransactionEffectuee = null;
			c.Tris = null;
			c.TypeBien = null;
			c.TypeChauffage = null;
			c.TypeTransaction = null;
			c.Ville = "";
		}



		private void BindData()
		{
			using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
			{

				client.Open();

				ServiceAgence.CriteresRechercheBiensImmobiliers c = new ServiceAgence.CriteresRechercheBiensImmobiliers();

				Initcriteres(c);

				if (IsPostBack)
				{

					c.TitreContient = Titre.Text;

					if (DropDownListTypeBien.SelectedValue != "-1")
					{
						Catalogue obj = new Catalogue();
						c.TypeBien = (ServiceAgence.BienImmobilierBase.eTypeBien)obj.AffectSelectedValue(DropDownListTypeBien);
					}

					c.AdresseContient = Adresse.Text;
					c.CodePostal = CP.Text;
					c.Ville = Ville.Text;

				}


				ServiceAgence.ResultatListeBiensImmobiliers res = client.LireListeBiensImmobiliers(c, 0, 8);


				List<ServiceAgence.BienImmobilierBase> liste = res.Liste.List;
				this.gvDisplay.DataSource = liste;
				this.gvDisplay.DataBind();


				client.Close();
			}
		}

		void FilterSubmit_Click(Object sender,   EventArgs e)
		{
			BindData();
		}

		protected void gvDisplay_RowEditing(object sender, GridViewEditEventArgs e)
		{
			gvDisplay.EditIndex = e.NewEditIndex;
			BindData();
		}
		protected void gvDisplay_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			gvDisplay.EditIndex = -1;
			BindData();
		}
		protected void gvDisplay_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{

			GridViewRow r = gvDisplay.Rows[e.RowIndex];

			Label id = (r.FindControl("lblId") as Label);

			TextBox titre = (r.FindControl("txtTitre") as TextBox);
			TextBox prix = (r.FindControl("txtPrix") as TextBox);
			TextBox montantCharges = (r.FindControl("txtMontantCharges") as TextBox);


			//CRUD

			using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
			{

				client.Open();

				ServiceAgence.BienImmobilier bien = client.LireDetailsBienImmobilier(id.Text).Bien;

				bien.Titre = titre.Text;
				bien.Prix = Convert.ToDouble(prix.Text);
				bien.MontantCharges = Convert.ToDouble(montantCharges.Text);



				client.ModifierBienImmobilier(bien);

				client.Close();
			}


			gvDisplay.EditIndex = -1;
			BindData();
		}



		protected void gvDisplay_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{

			GridViewRow r = gvDisplay.Rows[e.RowIndex];

			Label id = (r.FindControl("lblId") as Label);

			using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
			{

				client.Open();

				client.SupprimerBienImmobilier(id.Text);

				client.Close();
			}


			gvDisplay.EditIndex = -1;
			BindData();
		}

		protected void Page_Load(object sender, EventArgs e)
		{



			if (Session["Admin"] == null || Session["Admin"].ToString() == "false")
			{
				Response.Redirect("~/Connexion.aspx");
			}
			else
			{

				if (!IsPostBack)
				{
					
					Catalogue obj = new Catalogue();

					obj.Load_DropDownListItem<ServiceAgence.BienImmobilierBase.eTypeBien>(DropDownListTypeBien, true);
					DropDownListTypeBien.SelectedValue = "-1";
					
					BindData();
				}

				FilterSubmit.Click += new EventHandler(this.FilterSubmit_Click);
				
			}

		}
	}
}