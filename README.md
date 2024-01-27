# UnifiedApp

This example illustrates how to establish JWT token-based authentication in an ASP.NET Core Identity project using ASP.NET Core 8.

## Endpoints

- **Register:** Create new user accounts.
- **Login:** Log in and receive a JWT token.
- **Refresh Token:** Renew tokens without re-login.
- **Profile:** Access user-specific information.
![Endpoints](https://github.com/andissanayake/UnifiedApp/blob/master/images/endpoint.png?raw=true)


## Screens

- **Home:**
![Endpoints](https://github.com/andissanayake/UnifiedApp/blob/master/images/home.png?raw=true)
- **Register:**
![Endpoints](https://github.com/andissanayake/UnifiedApp/blob/master/images/register.png?raw=true)
- **Login:**
![Endpoints](https://github.com/andissanayake/UnifiedApp/blob/master/images/login.png?raw=true)
- **Unit Test:**
![Endpoints](https://github.com/andissanayake/UnifiedApp/blob/master/images/test.png?raw=true)
## Database Migrations

To add migrations, use the following command:

```bash
dotnet ef migrations add Initial --project src/data --startup-project src/api
```

To add Update Database, use the following command:

```bash
dotnet ef database update --project src/data --startup-project src/api
```
## Support

If you are having problems, please let me know by [raising a new issue](https://github.com/andissanayake/UnifiedApp/issues).

## License

This project is licensed with the [MIT license](LICENSE).
