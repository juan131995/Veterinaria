using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veterinaria
{
    [Activity(Label = "HistorialClinica")]
    public class HistorialClinica : Activity
    {
        EditText txtIdHistoriaMascota;
        EditText txtFecha;
        EditText txtNombreEspecialista;
        EditText txtMotivoConsultaMascota;
        EditText txtSintomasMascota;
        EditText txtProcedimientoMascota;
        EditText txtMedicamentoRecetado;
        EditText txtCantidadMedicamento;
        EditText txtHistorialVacunacion;
        EditText txtAlergia;
        EditText txtDetalleProcedimientoMascota;

        Button btnRegistrar;
        Button btnBuscar;
        Button btnActualizar;
        Button btnIrInicio;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HistoriaClinica);

            txtIdHistoriaMascota = FindViewById<EditText>(Resource.Id.txtIdHistoriaMascota);
            txtFecha = FindViewById<EditText>(Resource.Id.txtFecha);
            txtNombreEspecialista = FindViewById<EditText>(Resource.Id.txtNombreEspecialista);
            txtMotivoConsultaMascota =  FindViewById<EditText>(Resource.Id.txtMotivoConsultaMascota);
            txtSintomasMascota = FindViewById<EditText>(Resource.Id.txtSintomasMascota);
            txtProcedimientoMascota = FindViewById<EditText>(Resource.Id.txtProcedimientoMascota);
            txtMedicamentoRecetado = FindViewById<EditText>(Resource.Id.txtMedicamentoRecetado);
            txtCantidadMedicamento = FindViewById<EditText>(Resource.Id.txtCantidadMedicamento);
            txtHistorialVacunacion = FindViewById<EditText>(Resource.Id.txtHistorialVacunacion);
            txtAlergia = FindViewById<EditText>(Resource.Id.txtAlergia);
            txtDetalleProcedimientoMascota = FindViewById<EditText>(Resource.Id.txtDetalleProcedimientoMascota);

            btnRegistrar = FindViewById<Button>(Resource.Id.btnRegistrar);
            btnBuscar = FindViewById<Button>(Resource.Id.btnBuscar);
            btnActualizar = FindViewById<Button>(Resource.Id.btnActualizar);
            btnIrInicio = FindViewById<Button>(Resource.Id.btnIrInicio);

            btnRegistrar.Click += BtnCrearHistoria_Click;
            btnBuscar.Click += BtnConsultarHistoria_Click;
            btnActualizar.Click += BtnActualizarHistoria_Click;
            btnIrInicio.Click += BtnIrInicio_Click;

        }

        private void BtnIrInicio_Click(object sender, EventArgs e)
        {
            var bienvenido = new Intent(this, typeof(Bienvenido));
            StartActivity(bienvenido);
            Finish();

        }
        private Boolean ValidarCampos()
        {
            Boolean validar = false;
            if (!string.IsNullOrEmpty(txtFecha.Text.Trim()) && !string.IsNullOrEmpty(txtNombreEspecialista.Text.Trim())
                        && !string.IsNullOrEmpty(txtMotivoConsultaMascota.Text.Trim()) && !string.IsNullOrEmpty(txtSintomasMascota.Text.Trim())
                         && !string.IsNullOrEmpty(txtProcedimientoMascota.Text.Trim()) && !string.IsNullOrEmpty(txtMedicamentoRecetado.Text.Trim())
                          && !string.IsNullOrEmpty(txtCantidadMedicamento.Text.Trim()) && !string.IsNullOrEmpty(txtHistorialVacunacion.Text.Trim())
                           && !string.IsNullOrEmpty(txtAlergia.Text.Trim()) && !string.IsNullOrEmpty(txtDetalleProcedimientoMascota.Text.Trim()))
            {
                validar = true;
            }

            return validar;
            
        }

        private void BtnConsultarHistoria_Click(object sender, EventArgs e)
        {
            try
            {
                Historia_clinica resultado = null;
                if (!String.IsNullOrEmpty(txtIdHistoriaMascota.Text.Trim()))
                {
                    resultado = new Conectar().BuscarHistoria(int.Parse(txtIdHistoriaMascota.Text.Trim()));
                    if (resultado != null)
                    {
                        txtIdHistoriaMascota.Text = resultado.Idh.ToString();
                        txtNombreEspecialista.Text = resultado.Medico.ToString();
                        txtFecha.Text = resultado.Fecha.ToString();
                        txtMotivoConsultaMascota.Text = resultado.Motivo.ToString();
                        txtSintomasMascota.Text = resultado.Sintomas.ToString();
                        txtDetalleProcedimientoMascota.Text = resultado.Diagnostico.ToString();
                        txtMedicamentoRecetado.Text = resultado.Medicamento.ToString();
                        txtProcedimientoMascota.Text = resultado.Procedimiento.ToString();
                        txtCantidadMedicamento.Text = resultado.Dosis.ToString();
                        txtHistorialVacunacion.Text = resultado.HistorialVacunacion.ToString();
                        txtAlergia.Text = resultado.Alergia.ToString();

                        Toast.MakeText(this, "Consulta Exitosa", ToastLength.Short).Show();

                    }
                    else
                    {
                        Toast.MakeText(this, "El registro no existe", ToastLength.Short).Show();

                    }
                }
            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
        private void BtnCrearHistoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    
                    int num = new Conectar().Guardar(null,null,new Historia_clinica()
                    {
                        Idh=0,
                        Medico= txtNombreEspecialista.Text.Trim(),
                        Fecha = txtFecha.Text.Trim(),
                        Motivo = txtMotivoConsultaMascota.Text.Trim(),
                        Sintomas = txtSintomasMascota.Text.Trim(),
                        Diagnostico = txtDetalleProcedimientoMascota.Text.Trim(),
                        Medicamento = txtMedicamentoRecetado.Text.Trim(),
                        Procedimiento = txtProcedimientoMascota.Text.Trim(),
                        Dosis = txtCantidadMedicamento.Text.Trim(),
                        HistorialVacunacion = txtHistorialVacunacion.Text.Trim(),
                        Alergia = txtAlergia.Text.Trim(),

                    }, null, null);
                    if (num > 0)
                    {
                        Toast.MakeText(this, "Registro guardado con !Exito¡", ToastLength.Long).Show();
                        txtNombreEspecialista.Text = "";
                        txtFecha.Text = "";
                        txtMotivoConsultaMascota.Text = "";
                        txtSintomasMascota.Text = "";
                        txtDetalleProcedimientoMascota.Text = "";
                        txtProcedimientoMascota.Text = "";
                        txtCantidadMedicamento.Text = "";
                        txtHistorialVacunacion.Text = "";
                        txtAlergia.Text= "";
                        txtMedicamentoRecetado.Text = "";

                    }
                    else
                    {
                        Toast.MakeText(this, "Ocurrio un error no se pudo Guardar", ToastLength.Long).Show();
                    }
                    
                }
                else
                {
                    Toast.MakeText(this, "Rellene los campos requeridos, son obligatorios", ToastLength.Long).Show();
                }

            }
            catch (System.Exception ex)
            {

                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void BtnActualizarHistoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    if (!String.IsNullOrEmpty(txtIdHistoriaMascota.Text.Trim()))
                    {
                        int num = new Conectar().Guardar(null, null, new Historia_clinica()
                        {
                            Idh = int.Parse(txtIdHistoriaMascota.Text.Trim()),
                            Medico = txtNombreEspecialista.Text.Trim(),
                            Fecha = txtFecha.Text.Trim(),
                            Motivo = txtMotivoConsultaMascota.Text.Trim(),
                            Sintomas = txtSintomasMascota.Text.Trim(),
                            Diagnostico = txtDetalleProcedimientoMascota.Text.Trim(),
                            Medicamento = txtMedicamentoRecetado.Text.Trim(),
                            Procedimiento = txtProcedimientoMascota.Text.Trim(),
                            Dosis = txtCantidadMedicamento.Text.Trim(),
                            HistorialVacunacion = txtHistorialVacunacion.Text.Trim(),
                            Alergia = txtAlergia.Text.Trim(),

                        }, null, null);
                        if (num > 0)
                        {
                            Toast.MakeText(this, "Registro Actualizado  con !Exito¡", ToastLength.Long).Show();
                            txtNombreEspecialista.Text = "";
                            txtFecha.Text = "";
                            txtMotivoConsultaMascota.Text = "";
                            txtSintomasMascota.Text = "";
                            txtDetalleProcedimientoMascota.Text = "";
                            txtProcedimientoMascota.Text = "";
                            txtCantidadMedicamento.Text = "";
                            txtHistorialVacunacion.Text = "";
                            txtAlergia.Text = "";
                            txtMedicamentoRecetado.Text = "";

                        }
                        else
                        {
                            Toast.MakeText(this, "Ocurrio un error no se pudo Guardar", ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Rellene el campo ID ORDEN", ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Rellene los campos requeridos, son obligatorios", ToastLength.Long).Show();
                }

            }
            catch (System.Exception ex)
            {

                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}