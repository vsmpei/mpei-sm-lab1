# Приложение для создания и обработки матричных теоретико-игровых моделей
Данное приложение позволяет создавать матричную модель игры (как вручную, так и загрузкой из файла) детально ее настраивать, обрабатывать матрицу с помощью различных алгоритмов решения матричных игр, выгружать модель в файл. Также реализована поддержка истории изменений. На скриншоте представлен общий интерфейс, а далее следует обзор всех конкретных возможностей приложения.\
\
![interface](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/interface_new.PNG)
## Создание модели
При ручном создании модели нужно указать имена игроков и количество стратегий каждого игрока. Будет создана матрица, в которой указаны стандартные названия стратегий, а все веса равны нулю.
|Элемент приложения|Результат работы|
|:-:|:-:|
|![create](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/create.PNG)|![create_m](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/create_m_new.PNG)|
## Настройка модели
### Задание стратегий
Для ручного задания стратегии нужно указать имя игрока, номер новой стратегии и ее название. 
|Элемент приложения|Результат работы|
|:-:|:-:|
|![strategy](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/strategy.PNG)|![strategy_m](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/startegy_m.PNG)|
### Добавление весов
Для ручного добавления весов нужно указать названия стратегий обоих игроков, образующих ситуацию, и значение веса - выигрыш первого игрока в этой ситации. 
|Элемент приложения|Результат работы|
|:-:|:-:|
|![weight](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/weight.PNG)|![weigth_m](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/weight_m.PNG)|
## Решение игры
### Поиск максимина и минимакса
Принцип максимина - максимизация своего результата при наихудшем (для себя) выборе соперника. Максимин ищется для первого игрока. Для второго игрока ищется минимакс, обратный максимину. При совпадении максимина и минимакса это значение явлется седловой точкой, и ситуация считается возможным решением игры. В данном примере максимин и минимакс равны -4, и решением игры считается ситуация (Действие 3; Действие 2).
|Матрица игры|Результат решения|
|:-:|:-:|
|![maxmin_m](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/maxmin_m.PNG)|![maxmin](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/maxmin.PNG)|
### Удаление строго доминируемых стратегий
Принцип удаления строго доминируемых стратегий заключается в том, что игрок исключает стратегию, выигрыши которой в каждой ситуации меньше, чем в хотя бы одной другой стратегии. Последовательное поочередное исключение таких стратегий может привести к решению игры. В данном примере у каждого игрока последовательно дважды исключаются строго доминируемые стратегии, что приводит к решению игры в ситуации (Действие 2; Действие 1).  
|Начальная матрица|Первая итерация|Вторая итерация|
|:-:|:-:|:-:|
|![strogo_1](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/strogo_1.PNG)|![strogo_2](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/strogo_2.PNG)|![strogo_3](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/strogo_3.PNG)|
### Удаление слабо доминируемых стратегий
Принцип удаления слабо доминируемых стратегий заключается в том, что игрок исключает стратегию, выигрыши которой в каждой ситуации меньше либо равны выигрышам в хотя бы одной другой стратегии. Последовательное поочередное исключение таких стратегий может привести к решению игры. В данном примере у каждого игрока последовательно дважды исключаются слабо доминируемые стратегии, что приводит к решению игры в ситуации (Действие 2; Действие 1).  
|Начальная матрица|Первая итерация|Вторая итерация|
|:-:|:-:|:-:|
|![slabo_1](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/slabo_1.PNG)|![slabo_2](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/slabo_2.PNG)|![slabo_3](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/slabo_3.PNG)|
## Дополнительные функции
### Загрузка модели из файла
Модель можно загрузить из текстового файла следующего формата:
```
Количество стратегий:
<n - количество стратегий первого игрока> 
<m - количество стратегий второго игрока>
Никнеймы игроков:
<имя первого игрока>
<имя второго игрока>
Названия стратегий:
<название стратегии 1 для первого игрока>
...
<название стратегии n для первого игрока>
<название стратегии 1 для второго игрока>
...
<название стратегии m для второго игрока>
Матрица весов:
<a11> ... <a1m>
...
<an1> ... <anm>
```
|Содержание текстового файла|Результат загрузки|
|:-:|:-:|
|![load](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/load_new.PNG)|![load_m](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/load_m.PNG)|
### Выгрузка модели в файл
Файл выгрузки имеет тот же формат, что и файл загрузки. 
|Матрица игры|Содержание текстового файла|
|:-:|:-:|
|![save_m](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/save_m.PNG)|![save](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/save_new.PNG)|
### Отмена последнего действия
Можно отменить последнее действие, изменившее матричную модель.
### Заполнение матрицы случайными весами
Веса для каждой ситуации генерируются случайно из диапазона [-10; 10].
|Матрица игры до заполнения|Матрица игры после заполнения|
|:-:|:-:|
|![random_1](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/random_1.PNG)|![random_2](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/random_2.PNG)|
### История изменений
Приложение поодерживает просмотр истории изменений модели.\
\
![history](https://github.com/vsmpei/mpei-sm-lab1/blob/master/Images/history.PNG)

## Тестирование
[Таблица с результатами тестирования](https://docs.google.com/spreadsheets/d/1ELNSW0Z4XwTIyQLFF2ynXSetTYxT-mCHi1YjiBY5UJo/edit#gid=0)
