# U-Mandaditos Backend

Aplicación Backend de Sistema U-Mandaditos desarrollada con ASP.NET Core y Entity Framework Core.

## 🔧 Comandos iniciales

1. **Clonar el repositorio**
```bash
git clone https://github.com/kattherineh/U-Mandaditos.git
```

2. **Restaurar configuración** (ejecutar solo una vez):
```bash
dotnet tool restore
```

3. Agregar un archivo **.env** basado en example.env en el proyecto API

4. Crear las **migraciones**, en la consola de comandos ejecutar el siguiente comando
```bash
dotnet ef migrations add InitialMigration --project Infrastructure --startup-project API
```
  
3. Actualizar cambios en la **base de datos**
```bash
dotnet ef database update --project Infrastructure --startup-project API
```  
  
## 🐳 Uso de la Imagen Docker

1. Descargar la imagen
```bash
docker pull ghcr.io/kattherineh/u-mandaditos/api:dev
``` 

2. Crear y ejecutar el contenedor
 ```bash
docker run -d --name Nombre-Contenedor --env-file .env -p 8080:8080 Id-Imagen
```

### 📌 Notas importantes

- Asegúrarse de ejecutar el contenedor desde un directorio que contenga un archivo .env configurado correctamente.
- El puerto puede cambiarse modificando el parámetro -p (ej: -p 5000:8080)
- Para ver los logs del contenedor: docker logs Nombre-Contenedor


