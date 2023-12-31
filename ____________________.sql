-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Ноя 15 2023 г., 10:22
-- Версия сервера: 5.6.51
-- Версия PHP: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `библиотека`
--

-- --------------------------------------------------------

--
-- Структура таблицы `Автор книги`
--

CREATE TABLE `Автор книги` (
  `Код` int(11) NOT NULL,
  `ФИО автора` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Автор книги`
--

INSERT INTO `Автор книги` (`Код`, `ФИО автора`) VALUES
(1, 'Стивен Кинг'),
(2, 'Федор Достоевский'),
(3, 'Джордж Оруэлл'),
(4, 'Джейн Остин'),
(5, 'Лев Толстой'),
(6, 'Эрнест Хемингуэй');

-- --------------------------------------------------------

--
-- Структура таблицы `информацияпользователя`
--

CREATE TABLE `информацияпользователя` (
  `Код` int(11) NOT NULL,
  `Имя` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Фамилия` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Отчество` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Код адреса` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `информацияпользователя`
--

INSERT INTO `информацияпользователя` (`Код`, `Имя`, `Фамилия`, `Отчество`, `Код адреса`) VALUES
(1, 'Антон ', 'Белов ', 'Игоревич', 1),
(2, 'Денис ', 'Федоров ', 'Викторович', 2),
(3, 'Екатерина ', 'Лебедева ', 'Дмитриевна', 3),
(4, 'Татьяна ', 'Смирнова ', 'Андреевна', 4),
(5, 'Александра ', 'Попова ', 'Викторовна', 5);

-- --------------------------------------------------------

--
-- Структура таблицы `книга`
--

CREATE TABLE `книга` (
  `Код книги` tinyint(4) NOT NULL,
  `Название` varchar(14) DEFAULT NULL,
  `Жанр` varchar(69) DEFAULT NULL,
  `Год издательства` smallint(6) DEFAULT NULL,
  `Код автора` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `книга`
--

INSERT INTO `книга` (`Код книги`, `Название`, `Жанр`, `Год издательства`, `Код автора`) VALUES
(1, 'Оно', 'Роман,Литература ужасов, Тёмное фэнтези, История взросления', 1986, 1),
(2, 'Сияние', 'Роман,Литература ужасов, Готическая литература, Психологические ужасы', 1977, 1),
(3, 'Зеленая миля', 'Литература ужасов, Роман-фельетон, Магический реализм, Тёмное фэнтези', 1996, 1),
(4, 'Противостояние', 'Роман,Литература ужасов, Научная фантастика', 1978, 1),
(5, 'Мизеры', 'Роман, Литература ужасов, Психологические ужасы', 1987, 1),
(6, 'Мёртвая зона', 'Роман, Литература ужасов, Триллер, Научная фантастика', 1979, 1),
(7, 'Под Куполом', 'Роман, Литература ужасов, Научная фантастика', 2009, 1),
(8, 'Куджо', 'Роман, Литература ужасов', 1981, 1),
(9, 'Темная башня', 'Литература ужасов, Научная фантастика, Фэнтези', 2004, 1),
(10, 'Доктор сон', 'Фильм ужасов, Литература ужасов, Триллер, Саспенс', 2013, 1);

-- --------------------------------------------------------

--
-- Структура таблицы `адрес`
--

CREATE TABLE `адрес` (
  `Код` int(11) NOT NULL,
  `Дом` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Улица` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Город` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Страна` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `адрес`
--

INSERT INTO `адрес` (`Код`, `Дом`, `Улица`, `Город`, `Страна`) VALUES
(1, '123', 'Ленина', 'Минск', 'Беларусь'),
(2, '456', 'Победы', 'Витебск', 'Беларусь'),
(3, '678', 'Гагарина', 'Молодечно', 'Беларусь'),
(4, '234', 'Мира', 'Вилейка', 'Беларусь'),
(5, '234', 'Пушкина', 'Гомель', 'Беларусь');

-- --------------------------------------------------------

--
-- Структура таблицы `авторизация`
--

CREATE TABLE `авторизация` (
  `Код` tinyint(4) NOT NULL,
  `Логин` varchar(50) DEFAULT NULL,
  `Пароль` varchar(50) DEFAULT NULL,
  `Код информации пользователя` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `авторизация`
--

INSERT INTO `авторизация` (`Код`, `Логин`, `Пароль`, `Код информации пользователя`) VALUES
(1, 'Admin', '1111', 2),
(2, 'Пользователь1', 'Пользователь1', 3),
(3, 'Пользователь2', 'Пользователь2', 4),
(6, 'Пользователь3', 'Пользователь3', 5),
(7, 'Пользователь5', 'Пользователь5', 1);

-- --------------------------------------------------------

--
-- Структура таблицы `библиотекарь`
--

CREATE TABLE `библиотекарь` (
  `Код библиотекаря` tinyint(4) NOT NULL,
  `Имя` varchar(9) DEFAULT NULL,
  `Фамилия` varchar(8) DEFAULT NULL,
  `Отчество` varchar(13) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `библиотекарь`
--

INSERT INTO `библиотекарь` (`Код библиотекаря`, `Имя`, `Фамилия`, `Отчество`) VALUES
(1, 'Наталья', 'Пушкина', 'Николаевна'),
(2, 'Анна', 'Пчелкина', 'Михайловна'),
(3, 'Екатерина', 'Зайцева', 'Александровна'),
(5, 'Иван', 'Иванов ', 'Иванович'),
(6, 'Петр', 'Петров', 'Петрович'),
(7, 'Анна', 'Сидорова', 'Ивановна'),
(8, 'Алексей', 'Козлов', 'Владимирович'),
(9, 'Елена', 'Николаев', 'Сергеевна'),
(10, 'Дмитрий', 'Морозов', 'Александрович'),
(11, 'Ольга', 'Кузнецов', 'Васильевна');

-- --------------------------------------------------------

--
-- Структура таблицы `выдача`
--

CREATE TABLE `выдача` (
  `Код выдачи` tinyint(4) NOT NULL,
  `Дата оформления` varchar(19) DEFAULT NULL,
  `Дата возврата план` varchar(19) DEFAULT NULL,
  `Дата возврата факт` varchar(19) DEFAULT NULL,
  `Код книги` tinyint(4) DEFAULT NULL,
  `Код читателя` tinyint(4) DEFAULT NULL,
  `Код библеотекаря` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `выдача`
--

INSERT INTO `выдача` (`Код выдачи`, `Дата оформления`, `Дата возврата план`, `Дата возврата факт`, `Код книги`, `Код читателя`, `Код библеотекаря`) VALUES
(1, '2023-10-02 00:00:00', '2023-10-09 00:00:00', '2023-10-09 00:00:00', 3, 3, 2),
(2, '2023-10-01 00:00:00', '2023-10-08 00:00:00', '2023-10-09 00:00:00', 4, 6, 3),
(3, '2023-10-02 00:00:00', '2023-10-10 00:00:00', '2023-10-11 00:00:00', 1, 4, 2),
(4, '2023-10-05 00:00:00', '2023-10-13 00:00:00', '2023-10-12 00:00:00', 5, 7, 2),
(5, '2023-10-06 00:00:00', '2023-10-14 00:00:00', '2023-10-13 00:00:00', 3, 4, 1),
(6, '2023-10-02 00:00:00', '2023-10-16 00:00:00', '2023-10-16 00:00:00', 2, 2, 3),
(7, '2023-10-30 00:00:00', '2023-10-14 00:00:00', '2023-10-15 00:00:00', 6, 9, 1),
(8, '2023-10-08 00:00:00', '2023-10-15 00:00:00', '2023-10-16 00:00:00', 10, 8, 2),
(9, '2023-10-03 00:00:00', '2023-10-13 00:00:00', '2023-10-22 00:00:00', 9, 10, 3),
(10, '2023-10-04 00:00:00', '2023-10-13 00:00:00', '2023-10-13 00:00:00', 7, 5, 2);

-- --------------------------------------------------------

--
-- Структура таблицы `читатель`
--

CREATE TABLE `читатель` (
  `Код читателя` tinyint(4) NOT NULL,
  `Фамилия` varchar(50) DEFAULT NULL,
  `Имя` varchar(50) DEFAULT NULL,
  `Отчество` varchar(50) DEFAULT NULL,
  `Номер телефона` varchar(13) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `читатель`
--

INSERT INTO `читатель` (`Код читателя`, `Фамилия`, `Имя`, `Отчество`, `Номер телефона`) VALUES
(3, 'Полоз', 'Алексей', 'Непомнювич', '+375277387276'),
(4, 'Томкович', 'Даниил', 'Витальевич', '+375293405235'),
(5, 'Кербич', 'Никита', 'Незнаювич', '+375203857264'),
(6, 'Страх', 'Никита', 'Непомнювич', '+375263479126'),
(7, 'Талайко', 'Егор', 'Незнаювич', '+375261827364'),
(8, 'Млынарчик', 'Серафим', 'Незнаювич', '+375261928450'),
(9, 'Захаревич', 'Илья', 'Незнаювич', '+375836172394'),
(10, 'Лаптик', 'Александр', 'Незнаювич', '+375261934953');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `Автор книги`
--
ALTER TABLE `Автор книги`
  ADD PRIMARY KEY (`Код`);

--
-- Индексы таблицы `информацияпользователя`
--
ALTER TABLE `информацияпользователя`
  ADD PRIMARY KEY (`Код`),
  ADD KEY `Код адреса` (`Код адреса`);

--
-- Индексы таблицы `книга`
--
ALTER TABLE `книга`
  ADD PRIMARY KEY (`Код книги`),
  ADD KEY `Код автора` (`Код автора`);

--
-- Индексы таблицы `адрес`
--
ALTER TABLE `адрес`
  ADD PRIMARY KEY (`Код`);

--
-- Индексы таблицы `авторизация`
--
ALTER TABLE `авторизация`
  ADD PRIMARY KEY (`Код`),
  ADD KEY `Код информации пользоватля` (`Код информации пользователя`);

--
-- Индексы таблицы `библиотекарь`
--
ALTER TABLE `библиотекарь`
  ADD PRIMARY KEY (`Код библиотекаря`);

--
-- Индексы таблицы `выдача`
--
ALTER TABLE `выдача`
  ADD PRIMARY KEY (`Код выдачи`);

--
-- Индексы таблицы `читатель`
--
ALTER TABLE `читатель`
  ADD PRIMARY KEY (`Код читателя`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `Автор книги`
--
ALTER TABLE `Автор книги`
  MODIFY `Код` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT для таблицы `информацияпользователя`
--
ALTER TABLE `информацияпользователя`
  MODIFY `Код` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT для таблицы `книга`
--
ALTER TABLE `книга`
  MODIFY `Код книги` tinyint(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT для таблицы `адрес`
--
ALTER TABLE `адрес`
  MODIFY `Код` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT для таблицы `авторизация`
--
ALTER TABLE `авторизация`
  MODIFY `Код` tinyint(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT для таблицы `библиотекарь`
--
ALTER TABLE `библиотекарь`
  MODIFY `Код библиотекаря` tinyint(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT для таблицы `выдача`
--
ALTER TABLE `выдача`
  MODIFY `Код выдачи` tinyint(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT для таблицы `читатель`
--
ALTER TABLE `читатель`
  MODIFY `Код читателя` tinyint(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `информацияпользователя`
--
ALTER TABLE `информацияпользователя`
  ADD CONSTRAINT `информацияпользователя_ibfk_1` FOREIGN KEY (`Код адреса`) REFERENCES `адрес` (`Код`) ON DELETE CASCADE;

--
-- Ограничения внешнего ключа таблицы `книга`
--
ALTER TABLE `книга`
  ADD CONSTRAINT `книга_ibfk_1` FOREIGN KEY (`Код автора`) REFERENCES `автор книги` (`Код`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `авторизация`
--
ALTER TABLE `авторизация`
  ADD CONSTRAINT `авторизация_ibfk_1` FOREIGN KEY (`Код информации пользователя`) REFERENCES `информацияпользователя` (`Код`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
