using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Veterinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veterinaria
{
    [Activity(Label = "Mascota")]

    public class Mascota : Activity
    {
        EditText txtIdMascota;
        EditText txtNombreMascota;
        EditText txtCedulaDueño;
        EditText txtEdadMascota;
        EditText txtEspecieMascota;
        EditText txtRazaMascota;
        EditText txtCaracteristicasMascota;
        EditText txtPesoMascota;

        Button btnRegistrar;
        Button btnConsultar;
        Button btnIrInicio;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Mascotas);

            txtIdMascota = FindViewById<EditText>(Resource.Id.txtIdMascota);
            txtNombreMascota = FindViewById<EditText>(Resource.Id.txtNombreMascota);
            txtCedulaDueño = FindViewById<EditText>(Resource.Id.txtCedulaDueño);
            txtEdadMascota = FindViewById<EditText>(Resource.Id.txtEdadMascota);
            txtEspecieMascota = FindViewById<EditText>(Resource.Id.txtEspecieMascota);
            txtRazaMascota = FindViewById<EditText>(Resource.Id.txtRazaMascota);
            txtCaracteristicasMascota = FindViewById<EditText>(Resource.Id.txtCaracteristicasMascota);
            txtPesoMascota = FindViewById<EditText>(Resource.Id.txtPesoMascota);

            btnRegistrar = FindViewById<Button>(Resource.Id.btnRegistrar);
            btnConsultar = FindViewById<Button>(Resource.Id.btnConsultarPes);
            btnIrInicio = FindViewById<Button>(Resource.Id.btnIrInicio);

            btnRegistrar.Click += BtnCrearMascotas_Click;
            btnConsultar.Click += BtnConsultarMascota_Click;
            btnIrInicio.Click += BtnIrInicio_Click;


        }

        private void BtnIrInicio_Click(object sender, EventArgs e)
        {
            var bienvenido = new Intent(this, typeof(Bienvenido));
            StartActivity(bienvenido);
            Finish();

        }
            private void BtnCrearMascotas_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNombreMascota.Text.Trim()) && !string.IsNullOrEmpty(txtCedulaDueño.Text.Trim())
                        && !string.IsNullOrEmpty(txtEdadMascota.Text.Trim()) && !string.IsNullOrEmpty(txtEspecieMascota.Text.Trim())
                            && !string.IsNullOrEmpty(txtRazaMascota.Text.Trim()) && !string.IsNullOrEmpty(txtCaracteristicasMascota.Text.Trim()))
                {

                    int num = new Conectar().Guardar(null, new Mascotas()
                    {
                        Nombre_Mascota = txtNombreMascota.Text.Trim(),
                        Cedula_Amo = txtCedulaDueño.Text.Trim(),
                        Edad = txtEdadMascota.Text.Trim(),
                        Especie = txtEspecieMascota.Text.Trim(),
                        Raza = txtRazaMascota.Text.Trim(),
                        Caracteristicas = txtCaracteristicasMascota.Text.Trim(),
                        Peso = txtPesoMascota.Text.Trim()

                    }, null, null, null); ;
                    if (num > 0)
                    {
                        Toast.MakeText(this, "Registro guardado con !Exito¡", ToastLength.Long).Show();
                        txtNombreMascota.Text = "";
                        txtCedulaDueño.Text = "";
                        txtEdadMascota.Text = "";
                        txtEspecieMascota.Text = "";
                        txtCaracteristicasMascota.Text = "";
                        txtPesoMascota.Text = "";
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

        private void BtnConsultarMascota_Click(object sender, EventArgs e)
        {
            try
            {
                Mascotas resultado = null;
                if (!String.IsNullOrEmpty(txtCedulaDueño.Text.Trim()))
                {
                    resultado = new Conectar().BuscarMascota(txtCedulaDueño.Text.Trim());
                    if (resultado != null)
                    {
                        txtIdMascota.Text = resultado.Idm.ToString();
                        txtNombreMascota.Text = resultado.Nombre_Mascota.ToString();
                        txtEdadMascota.Text = resultado.Edad.ToString();
                        txtEspecieMascota.Text = resultado.Especie.ToString();
                        txtRazaMascota.Text = resultado.Raza.ToString();
                        txtCaracteristicasMascota.Text = resultado.Caracteristicas.ToString();
                        txtPesoMascota.Text = resultado.Peso.ToString();

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

    }
}