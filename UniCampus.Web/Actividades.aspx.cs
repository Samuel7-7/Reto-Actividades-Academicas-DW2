using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using UniCampus.Entities;
using UniCampus.Web.Presenters;
using UniCampus.Web.Views;

namespace UniCampus.Web
{
    public partial class Actividades : System.Web.UI.Page, IActividadesView
    {
        private ActividadesPresenter presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            presenter = new ActividadesPresenter(this);
            if (!IsPostBack)
            {
                presenter.Iniciar();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            presenter.Guardar();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblMensaje.Text = "";
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            presenter.Buscar();
        }

        protected void gvActividades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar") presenter.Editar(id);
            if (e.CommandName == "Eliminar") presenter.Eliminar(id);
        }

        public string TextoBuscar
        {
            get { return txtBuscar.Text; }
        }

        public int TipoFiltro
        {
            get { return Convert.ToInt32(ddlFiltroTipo.SelectedValue); }
        }

        public string EstadoFiltro
        {
            get { return ddlFiltroEstado.SelectedValue; }
        }

        public Actividad LeerFormulario()
        {
            Actividad a = new Actividad();
            a.ActividadId = Convert.ToInt32(hfId.Value);
            a.Titulo = txtTitulo.Text;
            a.Descripcion = txtDescripcion.Text;
            a.TipoId = Convert.ToInt32(ddlTipo.SelectedValue);
            a.Modalidad = ddlModalidad.SelectedValue;
            a.Lugar = txtLugar.Text;
            a.Estado = ddlEstado.SelectedValue;

            DateTime fecha;
            if (DateTime.TryParse(txtFechaInicio.Text, out fecha)) a.FechaInicio = fecha;
            if (DateTime.TryParse(txtFechaFin.Text, out fecha)) a.FechaFin = fecha;

            TimeSpan hora;
            if (TimeSpan.TryParse(txtHoraInicio.Text, out hora)) a.HoraInicio = hora;
            if (TimeSpan.TryParse(txtHoraFin.Text, out hora)) a.HoraFin = hora;

            int cupos;
            if (int.TryParse(txtCupos.Text, out cupos)) a.Cupos = cupos;

            return a;
        }

        public void CargarFormulario(Actividad a)
        {
            hfId.Value = a.ActividadId.ToString();
            txtTitulo.Text = a.Titulo;
            txtDescripcion.Text = a.Descripcion;
            ddlTipo.SelectedValue = a.TipoId.ToString();
            txtFechaInicio.Text = a.FechaInicio.ToString("yyyy-MM-dd");
            txtFechaFin.Text = a.FechaFin.ToString("yyyy-MM-dd");
            txtHoraInicio.Text = a.HoraInicio.ToString(@"hh\:mm");
            txtHoraFin.Text = a.HoraFin.ToString(@"hh\:mm");
            ddlModalidad.SelectedValue = a.Modalidad;
            txtCupos.Text = a.Cupos.ToString();
            txtLugar.Text = a.Lugar;
            ddlEstado.SelectedValue = a.Estado;
            btnGuardar.Text = "Actualizar";
        }

        public void LimpiarFormulario()
        {
            hfId.Value = "0";
            txtTitulo.Text = "";
            txtDescripcion.Text = "";
            ddlTipo.SelectedIndex = 0;
            txtFechaInicio.Text = "";
            txtFechaFin.Text = "";
            txtHoraInicio.Text = "";
            txtHoraFin.Text = "";
            ddlModalidad.SelectedIndex = 0;
            txtCupos.Text = "";
            txtLugar.Text = "";
            ddlEstado.SelectedIndex = 0;
            btnGuardar.Text = "Guardar";
        }

        public void MostrarTipos(List<TipoActividad> tipos)
        {
            ddlTipo.Items.Clear();
            ddlTipo.Items.Add(new ListItem("Seleccione", "0"));
            ddlFiltroTipo.Items.Clear();
            ddlFiltroTipo.Items.Add(new ListItem("Todos los tipos", "0"));
            foreach (TipoActividad t in tipos)
            {
                ddlTipo.Items.Add(new ListItem(t.Nombre, t.TipoId.ToString()));
                ddlFiltroTipo.Items.Add(new ListItem(t.Nombre, t.TipoId.ToString()));
            }
        }

        public void MostrarActividades(List<Actividad> actividades)
        {
            gvActividades.DataSource = actividades;
            gvActividades.DataBind();
        }

        public void MostrarMensaje(string mensaje, bool error)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = error ? "mensaje error" : "mensaje ok";
        }
    }
}
