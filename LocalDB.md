
---

### 🧱 Step 1: Open PostgreSQL as the default superuser

Most systems create a `postgres` superuser by default, so run:

```bash
sudo -u postgres psql
```

You should now see the prompt:

```
postgres=#
```

---

### 🧑‍💻 Step 2: Create your new user

Run this inside the psql shell:

```sql
CREATE ROLE "trackflow-admin" WITH LOGIN PASSWORD 'trackflow@123';
```

---

### 🧱 Step 3: Create your new database

Now run:

```sql
CREATE DATABASE "trackflow-dev" OWNER "trackflow-admin";
```

---

### 🧰 Step 4: Grant privileges

You can give your user full rights to the DB:

```sql
GRANT ALL PRIVILEGES ON DATABASE "trackflow-dev" TO "trackflow-admin";
```

---

### 🚪 Step 5: Exit psql

```sql
\q
```

---

### ✅ Step 6: Test the connection

Now try connecting with your new user:

```bash
psql -U trackflow-admin -d trackflow-dev
```

It’ll prompt for the password — enter:

```
trackflow@123
```

If you see a prompt like this:

```
trackflow-dev=>
```

you’re all set 🎯

---

### 🧩 Optional — Add it to your `appsettings.json`

In your `.NET` project, the connection string should look like:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=trackflow-dev;Username=trackflow-admin;Password=trackflow@123"
}
```

