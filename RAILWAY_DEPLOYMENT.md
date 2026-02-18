# Railway Deployment Guide for StudentProjectPlanner

## Quick Deployment Steps

### 1. **Prepare Your Railway Project**

- Make sure you have pushed these new files to GitHub:
  - `Dockerfile`
  - `.dockerignore`
  - Updated `railway.json`
  - Updated `Program.cs`

### 2. **Required Environment Variables in Railway**

Set these in your Railway project settings (Variables tab):

```bash
# Database connection (SQLite)
ConnectionStrings__DefaultConnection=Data Source=/home/data/StudentProjectPlanner.db

# ASP.NET Core Environment
ASPNETCORE_ENVIRONMENT=Production

# Port (Railway will set this automatically, but you can override)
PORT=5000

# Optional: Google OAuth (leave empty if not using)
Authentication__Google__ClientId=
Authentication__Google__ClientSecret=

# Optional: OpenWeatherMap API
OpenWeatherMap__ApiKey=
OpenWeatherMap__BaseUrl=https://api.openweathermap.org/data/2.5/
OpenWeatherMap__DefaultCity=Accra
```

### 3. **Deploy**

- Railway will automatically detect the Dockerfile and build
- The build should now succeed with .NET 8.0
- Your app will be available on a Railway-provided URL

### 4. **Volume for Database Persistence (Recommended)**

To persist your SQLite database across deployments:

1. In Railway dashboard, go to your service
2. Click on "Volumes" or "Storage"
3. Add a new volume:
   - **Mount Path**: `/home/data`
   - **Size**: 1GB (or as needed)

This ensures your database isn't lost on redeployments.

### 5. **Verify Deployment**

After successful deployment:

- Check the deployment logs for any errors
- Visit your Railway URL
- Test registration and login

## Troubleshooting

### Build Fails

- Check Railway logs for specific errors
- Ensure all files are committed to GitHub
- Verify the `Dockerfile` is in the root directory

### App Crashes on Startup

- Check environment variables are set correctly
- Verify the database path is writable
- Check Railway logs under "Deployments"

### Database Issues

- Ensure volume is mounted to `/home/data`
- Check that `ConnectionStrings__DefaultConnection` uses the correct path

## Local Docker Testing

Test the Docker build locally before deploying:

```bash
# Build the image
docker build -t studentprojectplanner .

# Run the container
docker run -p 5000:5000 -e ASPNETCORE_ENVIRONMENT=Production studentprojectplanner

# Access at http://localhost:5000
```

## Notes

- The app now uses SQLite in production by default
- Set `USE_SQLSERVER=true` if you want to use SQL Server instead
- Google OAuth is optional and only enabled if credentials are provided
