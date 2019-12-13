<script>
</script>

<style>
    .container {
        display: flex;

    }

    h1 {
        color: var(--color-text);
        font-family: 'Roboto', sans-serif;
        font-size: 20px;
    }

    h2 {
        color: var(--color-text);
        font-family: 'Roboto', sans-serif;
        font-size: 18px;
    }

    p {
        color: var(--color-text);
        font-family: 'Roboto', sans-serif;
        font-size: 16px;
        margin-top: 8px;
        margin-bottom: 8px;
    }

    ul {
        margin-top: 8px;
        margin-bottom: 8px;
    }

    li {
        color: var(--color-text);
        font-family: 'Roboto', sans-serif;
        font-size: 16px;
    }
</style>

<h1>О проекте</h1>
<p>
    Демо проект, состоящий из платформы для коллективного блога и тестов к ней.
</p>
<p>
    Git репозиторий: <a href="https://bitbucket.org/olmaad/demoblog-qa/src/master/">bitbucket.org</a>
</p>
<h2>Блог</h2>
<p>
    Возможности:
</p>
<ul>
    <li>Регистрация и авторизация пользователей</li>
    <li>Просмотр постов в ленте по дням и отдельно, в т.ч. по ссылке</li>
    <li>Публикация постов и комментариев</li>
    <li>Поддержка markdown в тексте постов</li>
    <li>Оценки постов и комментариев</li>
    <li>Рейтинг пользователей, строящийся на основе оценок, который влияет на порядок постов в ленте</li>
    <li>Навигация по внутренним ссылкам</li>
</ul>
<p>
    Состоит из браузерного клиента и сервера. Клиент написан с помощью js фреймворка svelte и собирается в 
    набор статичных ресурсов, которые может отдавать отдельный веб-сервер. Сервер - приложение на ASP.NET Core. 
    Он принимает запросы от клиента, построенные по принципам REST - будь то регистрация нового пользователя, или 
    запрос списка комментариев и пользователей, которые их написали. 
</p>
<h1>Тесты</h1>
<p>
    Здесь будет текст о том, как устроены тесты.
</p>
<h1>Ссылки</h1>
<ul>
    <li>Svelte (<a href="https://svelte.dev">svelte.dev</a>)</li>
    <li>Svelte-routing (<a href="https://github.com/EmilTholin/svelte-routing">github.com</a>)</li>
    <li>Remarkable (<a href="https://github.com/jonschlinkert/remarkable">github.com</a>)</li>
    <li>ASP.NET Core</li>
    <li>Roboto font (<a href="https://fonts.google.com/specimen/Roboto">fonts.google.com</a>)</li>
    <li>Font Awesome (<a href="https://fontawesome.com/">fontawesome.com</a>)</li>
</ul>