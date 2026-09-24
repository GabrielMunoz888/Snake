# Snake Game

Un clon de Snake hecho en Unity y C#, desarrollado como proyecto de práctica para consolidar fundamentos de C# y del motor Unity.

## Sobre el proyecto

Snake clásico con movimiento por grid, sprites orientables según la dirección de movimiento, sistema de puntuación y menú de fin de partida. El objetivo del proyecto fue aprender el ciclo completo de desarrollo de un juego: desde la lógica base hasta el pulido visual y sonoro.

## Características

- Movimiento por tick sobre un sistema de coordenadas de grid (`Vector2Int`)
- Cola de direcciones (`Queue`) para que los cambios de dirección rápidos no se pierdan entre ticks
- Bloqueo de giros de 180° para evitar colisiones instantáneas con el propio cuerpo
- Sprites dinámicos: el cuerpo cambia entre pieza recta y curva según la dirección de entrada/salida de cada segmento, calculado y rotado en tiempo real por código
- Cabeza y cola orientables según la dirección de movimiento
- Detección de colisiones con los bordes del tablero y con el propio cuerpo
- Sistema de puntuación con marcador en pantalla
- Pantalla de Game Over con puntuación final y opciones de reiniciar partida o salir
- Efectos de sonido y música de fondo

## Tecnologías

- Unity 6
- C#
- Unity Input System

## Controles

Flechas direccionales para mover la serpiente.

## Créditos

Assets de sonido: Pixabay
Música: [nombre de tu pack, si aplica]