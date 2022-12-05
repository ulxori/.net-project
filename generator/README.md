# Generator danych z czujników

Libki potrzebne: https://drive.google.com/file/d/1UkwBtpTstxiJ25SQdHr-bP1cZkD2Py9B/view?usp=sharing

JavaFX-16 (w Intellij podajemy ścieżkę do lib, ja wrzuciłem cały katalog SDK)     // GUI

- json-simple-1.1.1.jar          // do ładnego zapisu JSONów
- amqp-client-5.16.0.jar         // klient RabbitMQ
- log4j-1.2.17.jar	       // nie wiem czemu ale klient Rabbita tego wymaga
- slf4j-api-1.7.5.jar            // jw.
- slf4j-log4j-1.7.5.jar          // jw.

- w ustawieniach projektu
  
  --module-path "C:\Program Files\Java\javafx-sdk-16\lib" --add-modules=javafx.controls,javafx.fxml
  
(oczywiście ścieżka może być inna)



Jak dodać JAR'a do projektu?
File -> Project Structure -> Project Settings -> Libraries


Przykład ustawień w IntelliJ:
![src1](https://user-images.githubusercontent.com/38257808/200410144-7e843222-1d36-4833-b888-e4ba195161d4.png)
![src2](https://user-images.githubusercontent.com/38257808/200410157-bee6f084-cbb2-4fee-9974-35c76c1ef2db.png)
