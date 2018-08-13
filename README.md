# Koyomi

Koyomi is a web app that allows you to add the weekly anime you're watching to your Google Calendar. It's built with .NET Core and Vue.js.

## Getting Started

### Backend

```bash
# KoyomiAPI is project you usually want to run
cd KoyomiAPI/

# run the development server 
dotnet watch run

# build for production and put the output in bin/Release/
dotnet publish --configuration Release
```

### Frontend

```bash
# install dependencies
yarn 

# run the development server 
yarn serve 

# build for production and put the output in dist/
yarn build 
```

## Built With

### Backend

- .NET Core
- ASP.NET Core (for the API project)

### Frontend

- Vue.js
- Sass
- Webpack
- Yarn


## Authors

* **Veselin Romić (OmegaVesko)**

## License

This project is licensed under the GPL v3 - see the [LICENSE](LICENSE) file for details.
