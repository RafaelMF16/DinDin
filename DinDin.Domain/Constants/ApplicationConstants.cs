namespace DinDin.Domain.Constantes
{
    public static class ApplicationConstants
    {
        public const string CONNECTION_STRING_ENVIRONMENT_VARIABLE = "dindinConnectionString";
        public const string SERVER_URL_ENVIRONMENT_VARIABLE = "serverURL";
        public const string DATABASE_NAME_ENVIRONMENT_VARIABLE = "databaseName";
        public const short WORK_FACTOR = 12;
        public const string SECRET_KEY_ENVIRONMENT_VARIABLE = "secretKey";
        public const string APP_SETTINGS_NAME = "appsettings.json";
        public const string CORS_POLICY_NAME = "MyPolicy";
        public const string FRONT_END_URL = "http://localhost:57053";
        public const char ID_SEPARATION_PATTERN = '-';
        public const string INTERNAL_SERVER_ERROR_TITLE = "Ocorreu um erro inesperado.";
        public const string VALIDATION_EXCEPTION_TITLE = "Erro de validação";
        public const string VALIDATION_EXTENSIONS_NAME = "errors";
        public const string BAD_REQUEST_ERROR_TITLE = "Requisição incorreta";
        public const string AUTHENTICATION_ERROR_TITLE = "Erro de autenticação";
        public const string AUTHENTICATION_ERROR_MESSAGE = "Email ou senha incorreta";
    }
}