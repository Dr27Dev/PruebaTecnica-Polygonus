# **PruebaTecnica-Polygonus**
> Simón Osorio Uribe

Build: https://drive.google.com/drive/u/2/folders/1B3xhNkQ_4nJVnA9SkzFnJlFdGGRLzvu6

Desarrollé un minijuego en vista Top-Down, con temática arcade shooter.

----
## *Jugador*

Controlamos una nave que puede moverse tanto horizontal como verticalmente por la pantalla.
La nave podrá disparar dos tipos de proyectiles, uno `verde` y uno `rojo`.

Con tan solo recibir un solo golpe, el jugador perderá y se reiniciará el juego.

## *Enemigos*

![image](https://github.com/Dr27Dev/PruebaTecnica-Polygonus/assets/108661855/1e084ebb-ef7e-49a7-8a12-907acc7478d9)
![image](https://github.com/Dr27Dev/PruebaTecnica-Polygonus/assets/108661855/d647948f-d8ae-492b-9837-a7189d25b269)

Hay dos tipos de enemigos, uno `verde` y uno `rojo`, al igual que los proyectiles del jugador. Cada enemigo solo es afectado por los proyectiles de su color.
Adicionalmente, hay dos tipos de movimiento, que se seleccionan aleatoriamente:
- Circular: El enemigo bajará por la pantalla mientras va trazando una circunferencia.
- Hacia el jugador: El enemigo tomará una posición en la que está el jugador, e irá hacia ella. Una vez llegue a esa posición, calculará de nuevo la posición del jugador, y volverá a comenzar el ciclo.

Los enemigos también dispararán mientras van bajando, además de que pueden aparecer en *hordas* de diferentes cantidades dependiendo del puntaje actual (sistema de progresión/dificultad).

## *Puntaje*

Para conseguir puntos, el jugador deberá destruir los enemigos con su respectivo proyectil. Cada que un enemigo sea destruido, otorgará 8 puntos.
Además, el jugador podrá recolectar monedas por el mapa que le otorgarán de a 5 monedas cada una.

----

## *Sistemas Utilizados*

Tanto los enemigos como la munición y los pickups están basados en un sistema de pooling en el que se crean todos los objetos al inicio del juego, y se van utilizando a necesidad (activnado/desactivando).
Además también se utilizó un Singleton en diferentes scripts.

Para el apartado gráfico, hay shaders y efectos visuales notorios:
- Pixelización de la pantalla: En realidad es una render feature que se encarga de dar un efecto de pixelado, simplificando diferentes puntos de la pantalla y amplificándolos a placer. Todo lo que hay al fondo son simplemente shaders y modelos 3D.

 ![image](https://github.com/Dr27Dev/PruebaTecnica-Polygonus/assets/108661855/0512bbf5-069f-41b9-9077-3cfc3bc64fa9)
 ![image](https://github.com/Dr27Dev/PruebaTecnica-Polygonus/assets/108661855/31ccf924-59d5-4f81-8c40-3caf7209c921)


- Agua: Combinación de movimiento de UV's con texturas simulando cáusticas.
- Postprocessing: Bloom, Vignette, Chromatic Aberration, Lens Distortion

## *Captura del Proyecto In-Game*
![image](https://github.com/Dr27Dev/PruebaTecnica-Polygonus/assets/108661855/85065cea-a489-40e1-ad78-c5d345adbdfb)
