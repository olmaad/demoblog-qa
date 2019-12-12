# Тестирование

Во время тестирования нужно проверить, что продукт выполняет свою основную задачу, не имеет дефектов, мешающих ему это делать и соответствует предъявленым ему требованиям.

## Исходные данные

Набор исходных знаний о предмете тестирования, опираясь на которые составляется тест-план.

### Идея продукта

Основная цель работы этой платформы для блога - возможность размещать на ней публикации, читать их, приоритезировать заслуживающие внимания (с помощью оценок), и общаться по поводу них. Если что-то мешает это делать - это дефект, будь то отсутсвующая функция или сломанный интерфейс.

### Функциональные требования

Полностью требования описаны в другом документе. Вот их краткий список:

* Авторизация пользователей
* Публикация поста
* Просмотр постов и комментариев
* Комментирование поста
* Оцениваение постов и комментариев
* Рейтинг
* Ссылки

## Список тестов

### Основные

| Название | Комментарий | Автотест |
|------------------------------|------------------------------|:---:|
| Визуальная проверка всего интерфейса | Все ли нормально с интерфейсом? Не поехала ли верстка? Все функции доступны визуально? | Нет |
| Тест всех GET методов api на тестовых данных | Возращает ли api верные данные? | Да |
| Тест всех модифицирующих методов api | Работают ли модификация/сохранение данных на сервере? Возвращает ли он обновленные данные после этого? | Да |
| Негативный тест модифицирующих методов api | Как обрабатываются ошибки и неверные данные на сервере? | Да |

### Пользователи

| Название | Комментарий | Автотест |
|------------------------------|------------------------------|:---:|
| Визуальный тест |  | Нет |
| Регистрация нового пользователя | Удается ли зарегистрировать нового пользователя? Может ли он потом авторизоваться? | Да |
| Негативный тест регистрации | Обрабатываются ли ошибки регистрации - неверные данные, существующий пользователь и т.п.? | Да |
| Авторизация существующего пользователя | Может ли авторизоваться существующий пользователь изначально? Если уже авторизован другой пользователь? | Да |
| Негативный тест авторизации | Как обрабатываются ошибки авторизации? | Да |

### Публикация поста

| Название | Комментарий | Автотест |
|------------------------------|------------------------------|:---:|
| Визуальный тест |  | Нет |
| Функции редактора | Работает ли предпросмотр? | Да |
| Публикация поста | Можно ли опубликовать пост? Доступе ли он после этого для просмотра? | Да |
| Негативный тест публикации | Можно ли опубликовать пост с недостаточной информацией (отсутствует заголовок/текст)? Может ли опубликовать пост неавторизованный пользователь? | Да |

### Просмотр постов и комментариев

| Название | Комментарий | Автотест |
|------------------------------|------------------------------|:---:|
| Визуальный тест списка постов, просмотрщика поста, списка комментариев |  | Нет |
| Функции списка постов | Можно ли открыть просмотр поста? Работает ли голосование? Работают ли ссылки? | Да |
| Совпадение с тестовыми данными | Совпадает список постов с тестовыми данными? Совпадает ли пост с тестовыми данными (при просмотре поста)? Совпадают ли комментарии с тестовыми данными? | Да |

### Комментирование поста

| Название | Комментарий | Автотест |
|------------------------------|------------------------------|:---:|
| Визуальный тест редактора комментариев |  | Нет |
| Публикация комментария | Можно ли опубликовать комментарий? Виден ли он после этого? | Да |
| Негативный тест публикации | Можно ли опубликовать пустой комментарий? Может ли опубликовать комментарий неавторизованный пользователь? | Да |

### Оценивание постов и комментариев

| Название | Комментарий | Автотест |
|------------------------------|------------------------------|:---:|
| Визуальный тест |  | Нет |
| Функции | Можно ли поставить оценку? Можно ли поменять/отменить оценку? Видно ли оценку после? Видно ли оценку другим пользователям? | Да |
| Негативный тест оценок | Может ли поставить оценку неавторизованный пользователь? | Да |

### Рейтинг

| Название | Комментарий | Автотест |
|------------------------------|------------------------------|:---:|
| Тест влияния оценок на порядок постов | Влияют ли оценки на порядок постов? А оценки комментариев? Влияют ли оценки на рейтинг автора поста? | Да |
| Тест персонального рейтинга пользователь-пользователь | Учитывается ли персональный рейтинг? | Да |

### Ссылки

| Название | Комментарий | Автотест |
|------------------------------|------------------------------|:---:|
| Разделы | Можно ли открывать разделы (список постов, редактор), по ссылкам? | Да |
| Посты | Отрываются ли посты по ссылке? | Да |
| Негативный тест | Как обрабатываются неверные ссылки? | Да |