# EjemploReporteGenericoItextsharp
Generá un reporte genérico con itextsharp y recibe parámetros por medio de un JSON

# Paramétros de entrada: 
## Estructura de JSON
	"{
	   \"tipoPdf\":1,
	   \"datosConexion\":
	   {
	    	   \"ipServidor\":\"ip\",
		   \"BaseDeDatos\":\"base de datos\",
		   \"puerto\":\"puerto sql o postgresql\",
		   \"usuario\":\"usuario\",
		   \"contrasenia\":\"contraseña\",
		   \"opcManejadorBaseDeDatos\":int
	   },
	   \"orientacionHoja\":int,
	   \"nom_archivo\":\"nombre de tu archivo.pdf\",
	   \"rutaArchivo\":\"C:\\Users\\tu_pc\\OneDrive\\Documentos\\Archivos\",
	   \"tituloArchivo\":\"Tablas de Pg_tables\",
	   \"consulta\":\"select * from pg_tables\"
	}"

## Detalle de los enumeradores (int): 
- opcManejadorBaseDeDatos = {SqlServer = 0, Postgresql = 1}
- orientacionHoja = {vertical = 1, horizontal = 2}
