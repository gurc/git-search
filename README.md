# git-search
Для работы приложения необходимо сгенерировать "Personal Access Token (PAT)" согласно данной инструкции:

https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token

Сгенерированный PAT необходимо "захардкодить" как свойство `token` класса `GitSearch.Utils.Utils`.
Хранение токена в конфигурационном файле или использование другого механизма авторизации пока не реализовано.
