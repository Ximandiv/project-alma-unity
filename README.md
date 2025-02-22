# Proyecto Alma G2

Proyecto práctico y final del curso Desarrollo de Videojuegos con Academy.
Consistente en una protagonista del mismo nombre que del juego, psicóloga
rural que busca desmitificar la salud mental en un pueblo.

## Herramientas
- Unity 6 (6000.0.30f1)
- Cualquier IDE (Visual Studio 2022, Visual Studio Code, Rider, ...)

## Assets de Terceros
- Stardew Farm Game Assets | Top-Down Pixel Art - nogardev https://nogardev.itch.io/stardew-farm-pixel-art-top-down-assets
- The fantasy tileset - Ventiratore https://ventilatore.itch.io/the-fantasy-tileset
- Pipoya Free RPG Tileset 32x32 Pixel - Pipoya https://pipoya.itch.io/pipoya-rpg-tileset-32x32
- Harmonious Seasons - Foxtale Studios https://foxtalestudios.itch.io/harmonious-seasons
- Solaria Farm Animal Sprites Free Version - Jamiebrownhill https://jamiebrownhill.itch.io/solaria-farm-animal-sprites
- Mobile Power Ups Free Vol.1 / VisCircle https://assetstore.unity.com/packages/3d/props/mobile-power-ups-free-vol-1-36106
- Hyper Casual FX / Lana Studio https://assetstore.unity.com/packages/vfx/particles/hyper-casual-fx-200333
- Piloto Studio Shaders / Piloto Studio https://assetstore.unity.com/packages/vfx/shaders/piloto-studio-shaders-258376
- Ultimate Loot VFX Pack ⚜ 175 Effects / piloto Studio https://assetstore.unity.com/packages/vfx/particles/ultimate-loot-vfx-pack-175-effects-242936
- RPG Essentials Sound Effects / leohpaz https://assetstore.unity.com/packages/audio/sound-fx/rpg-essentials-sound-effects-free-227708
- Monster SFX - 111518 / GWriterStudio https://assetstore.unity.com/packages/audio/sound-fx/monster-sfx-111518-132868
- Jungle Animal Sound FX / Laali Unit https://assetstore.unity.com/packages/audio/sound-fx/animals/jungle-animal-sound-fx-13491
- Fanfare: Grand Entrance / Band of the Royal Irish Regiment https://www.youtube.com/watch?v=HtKsV-HO82U&ab_channel=BandoftheRoyalIrishRegiment-Topic
- Tema Aggressive epic ROCK Track | music no copyright RPm 99 https://www.youtube.com/watch?v=qycUwwI43bY&ab_channel=RPm99
- EPIC BOSS BATTLE / Epic Cinematic Music / Emotional Dramatic Background Music / No Copyright / NORTH-7 Epic Music https://www.youtube.com/watch?v=iBkJz38wQMw&ab_channel=NORTH-7EpicMusic
- 2D Monster Wizard / JKTimmons https://assetstore.unity.com/packages/2d/characters/2d-monster-wizard-184692
- 100 Fantasy Characters Mega Pack / Blackthornprod https://assetstore.unity.com/packages/2d/characters/100-fantasy-characters-mega-pack-222143

## Integrantes
- Emmanuel Vanegas Carmona (ximandiv@gmail.com)
- Juana María Rodas Álvarez (jrodas033@gmail.com)
- Santiago Quintero Hincapié (sanquih01@gmail.com)
- Felipe Bedoya Pizarro (raventekto@proton.me)
- Jonathan Martin Albert Silva (albertcorp@gmail.com)
- Sergio Alejandro Pérez Muñoz (saperezm1@eafit.edu.co)
- Santiago Luna (lunita0239@gmail.com)

## Documentos
- [Documentos Grupales](https://drive.google.com/drive/u/1/folders/1BFTzKYWurZSedsXv4HuVGfB7Crc_83wp)
- [GDD](https://docs.google.com/document/d/1c_LRxygZxJ7mgLa41zeE9S62p9FzXL7W0LxeGT6vOPU/edit?usp=sharing)
- [Ideas Iniciales](https://docs.google.com/document/d/1drn2SEzIfjNAMbwFOVutIBCGx5wWuJiZxX3pGG7jDxE/edit?tab=t.0)

## Convenciones

- En el proyecto existe una escena llamada TestScene. Siempre trabajen en esta cuando esten desarrollando. Al terminar el desarrollo, pasen lo creado a la escena que le corresponde el trabajo.

- Los mensajes de commit deben de ser cortos y con verbos infinitivos; por ejemplo: Adds PlayerMovement class

### Código
- Todo el código debe de estar en inglés.

- Las clases no deben tener más de 7 variables o métodos.

- Los métodos no deben tener más de 7 parámetros.

- Los métodos y variables privadas deben ir siempre en camel case, es decir: 
```
private int amountOfLives;
```

- Los métodos y variables públicas deben de ir siempre en pascal case, es decir:
```
public int AmountOfLives;
```

- Toda clase debe llevar el namespace que sea igual a la ubicación en carpeta; por ejemplo: 
```text
namespace Scripts.Player; 

public class Player
{

}
```

- El ordén de acceso en métodos y variables debe ser: Variables Públicas, Métodos Públicos, Variables Privadas/Protected, Métodos Privados/Protected. Por ejemplo:
```
namespace Scripts.Player;

public class Player
{
    public int HitPoints;

    public void AddHitPoints()
    {

    }

    private int lives;

    private bool isAlive()
    {

    }
}
```

- Toda clase que sea única sin relación a otras, debe estar adentro de la carpeta Scripts/. La única excepción es si es un Manager.

- Los Manager son clases cuya intención es el manejo de un sistema y no debe interferir en las clases internas para el funcionamiento del juego. Por ejemplo; no deberia modificar el jugador bajo ninguna circumstancia, solo es responsable de un sistema y como se propaga para las otras clases que no dependen del Manager.

- Las clases deben de estar responsables de solo una cosa a la vez; por ejemplo: PlayerMovement, PlayerVariables, etc.

- No pueden haber godclasses (Que manejan 2 o más responsabilidades) bajo ninguna circumstancia.

- Usar MVC para convenciones de UI. Por ejemplo; un controller en el canvas que de acuerdo a la vista (imágen o texto) cambia según el modelo (entidad, por ejemplo, un jugador) que dicta el controller.

- Los nombres de las variables y métodos deben de ser descriptivos con verbos o nombres que lo denoten completos y nunca usar abreviaciones; por ejemplo:
```
public MoveTowards(Vector2 targetPosition)
{

}

private int hitPoints;
```

- Los nombres no pueden ser muy largos, si tienen 4 o más palabras, hay que buscar acortarlos.

- Todas las variables y métodos booleanos en su nombre deben responder una pregunta; por ejemplo:
```
private bool isAlive = false;

private bool isJumping()
{

}
```

- No poner el nombre de la entidad en la clase; por ejemplo: Si trabajas en un namespace y clase perteneciente al jugador, no es necesario llamar las variables con prefijo de 'Player', sino directamente lo que son. Por ejemplo:

```
namespace Scripts.Player;

public class Player
{
    public int HitPoints;

    public void AddHitPoints()
    {

    }

    private int lives;

    private bool isAlive()
    {

    }
}
```
