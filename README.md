# git-search
Для работы приложения необходимо сгенерировать Personal Access Token (PAT) на Github согласно данной инструкции:

https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token

Сгенерированный PAT необходимо прописать в свойстве `token` файла `token.json`.

Проект поставляется без базы данных (БД). 
Необходимо создать её с помощью команды `Update-Database`, либо при первом использовании БД будет сгенерирована автоматически.

Для REST API `GET /api/find` реализована пагинация. 
Для доступа к конкретной странице необходимо использовать адрес вида `/api/find/{page}`. 
Количество результатов на странице задаётся параметром `ResultsOnPage` в файле `appsettings.json`. 
Сейчас установлено значение 2 (для удобства тестирования).
