# Requisitos
	.NET SDK 5.0
	MySQL Server >= 8.0 (Development Env) || MariaDB >= 10.5 (Development Env)
	Visual Studio 2019

# Instrucciones
* Instalar el certificado "localhost.p12" dentro de las Entidades de Certificación Raíz de Confianza (Trusted Root Certification Authorities)
* Abrir la solución en Visual Studio 2019
* Modificar en la solución las opciones de inicio de los proyectos a ejecución de múltiples proyectos con el siguiente orden : LionStore.Identity, LionStore.Api
* Ejecutar los proyectos en modo Debug (Usando Kestrel y no IIS)

# Auth
	Usuario		Contraseña
	admin		Pass123$
	seller		Pass123$
	client		Pass123$

PD
* El enum del estado de la Orden se encuentra en "LionStore.Data\Models\Order.cs"
* Los puertos y Db Connection Strings están Hardcoded para comodidad a la hora de desarrollar.
* El Identity Server (HTTPS Only) correrá sobre el puerto (44310) mientras que la API (HTTPS Only) sobre (44311), De ser necesario cambiar el puerto del IdentityServer, reflejar el cambio en "appsettings.json" de la API. Los puertos están Hardcoded en cada Program.cs
* De ser necesario cambiar los Db Connection Strings en Identity Server (Startup.cs Línea 32) y API (Startup.cs Línea 71)
* Las migraciones se ejecutarán automáticamente al lanzar "LionStore.Identity" por lo cual es probable que la API lance una excepción en el 1er inicio, ya que el Identity Server se encuentra migrando la DB (En este caso esperar unos segundos para que termine de migrar y reiniciar ambos proyectos).
* Es probable que funcione en versiones anteriores de MySql Server y MariaDB, las referencias son solo en el entorno de Desarrollo
