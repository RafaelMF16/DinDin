const express = require('express');
const path = require('path');

const app = express();
const port = 8080;

const locales = ['pt', 'en', 'es'];

locales.forEach(locale => {
  const staticPath = path.join(__dirname, 'dist', 'din-din.angular-project', 'browser', locale);

  app.use(`/${locale}`, express.static(staticPath));

  app.get(new RegExp(`^/${locale}(/.*)?$`), (req, res) => {
    res.sendFile(path.join(staticPath, 'index.html'));
  });
});

app.get('/', (req, res) => {
  res.redirect('/pt/');
});

app.listen(port, () => {
  console.log(`Servidor rodando: http://localhost:${port}`);
});