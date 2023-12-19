Create a Postgres database (I prefer through docker).

Here is guide that covers all commands, so you can do it in few clicks.
```
docker volume create pgdata
```

```
docker run -p 5432:5432 --name postgres -v pgdata:/var/lib/postgresql/data -e POSTGRES_PASSWORD=root -e POSTGRES_USER=root -d postgres
```

I am using react on this project

Please run 
```
npm install
```
```
npm start
```
