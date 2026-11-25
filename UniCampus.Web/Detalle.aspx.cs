using System;
using System.Collections.Generic;
using UniCampus.Entities;
using UniCampus.Web.Presenters;
using UniCampus.Web.Views;

namespace UniCampus.Web
{
    public partial class Detalle : System.Web.UI.Page, IDetalleView
    {
        private DetallePresenter presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            presenter = new DetallePresenter(this);
            if (!IsPostBack)
            {
                presenter.Cargar();
            }
        }

        protected void btnInscribirme_Click(object sender, EventArgs e)
        {
            presenter.Inscribirme();
        }

        protected void btnPublicar_Click(object sender, EventArgs e)
        {
            presenter.PublicarResena();
        }

        public int ActividadId
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["id"], out id);
                return id;
            }
        }

        public int UsuarioId
        {
            get { return (int)Session["UsuarioId"]; }
        }

        public int Calificacion
        {
            get { return Convert.ToInt32(ddlCalificacion.SelectedValue); }
        }

        public string Comentario
        {
            get { return txtComentario.Text; }
        }

        public void MostrarActividad(Actividad a, bool estaInscrito)
        {
            lblTitulo.Text = Server.HtmlEncode(a.Titulo);
            lblTipo.Text = a.Tipo;
            lblDescripcion.Text = Server.HtmlEncode(a.Descripcion);
            lblFecha.Text = a.FechaInicio.ToString("dd/MM/yyyy") + " - " + a.FechaFin.ToString("dd/MM/yyyy");
            lblHora.Text = a.HoraInicio.ToString(@"hh\:mm") + " - " + a.HoraFin.ToString(@"hh\:mm");
            lblModalidad.Text = a.Modalidad;
            lblLugar.Text = Server.HtmlEncode(a.Lugar);
            lblCupos.Text = a.Disponibles + " disponibles de " + a.Cupos;
            lblEstado.Text = a.Estado;

            if (estaInscrito)
            {
                btnInscribirme.Text = "Ya estás inscrito";
                btnInscribirme.Enabled = false;
            }
        }

        public void MostrarInscritos(List<Inscripcion> inscritos)
        {
            gvInscritos.DataSource = inscritos;
            gvInscritos.DataBind();
        }

        public void MostrarResenas(List<Resena> resenas)
        {
            gvResenas.DataSource = resenas;
            gvResenas.DataBind();
        }

        public void LimpiarResena()
        {
            txtComentario.Text = "";
        }

        public void MostrarMensaje(string mensaje, bool error)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = error ? "mensaje error" : "mensaje ok";
        }
    }
}
