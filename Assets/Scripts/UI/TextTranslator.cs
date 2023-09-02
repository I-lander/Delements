using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTranslator : MonoBehaviour
{
    public TMP_Text credits;
    public TMP_Text start;
    public TMP_Text online;
    public TMP_Text tuto;
    public TMP_Text tutoText;
    public TMP_Text back;
    public TMP_Text quit;

    public TMP_Text lobbyTitle;
    public TMP_Text helperCreate;
    public TMP_Text createButton;
    public TMP_Text createPlaceHolder;
    public TMP_Text helperJoin;
    public TMP_Text joinButton;
    public TMP_Text joinPlaceHolder;
    public TMP_Text backButton;
    public TMP_Text loading;

    public TMP_Text waitingRoom;
    public TMP_Text disconnectedRoom;
    public TMP_Text quitButtonPause;
    public TMP_Text quitButtonWin;
    public TMP_Text quitButtonDisconnected;
    public TMP_Text resumeButton;

    private Dictionary<string, LocalizedValue> localizedData = new Dictionary<string, LocalizedValue>
    {
        {
    "credits", new LocalizedValue{
        fr="Créé et Désigné par <b>Ilan V</b>",
        en="Created and Designed by <b>Ilan V</b>",
        es="Creado y Diseñado por <b>Ilan V</b>",
        it="Creato e Progettato da <b>Ilan V</b>",
        de="Erstellt und Designed von <b>Ilan V</b>",
        pt="Criado e Projetado por <b>Ilan V</b>"
        }
    },
    {
    "start", new LocalizedValue{
        fr="Jouer",
        en="Play",
        es="Jugar",
        it="Gioca",
        de="Spielen",
        pt="Jogar"
        }
    },
    {
    "online", new LocalizedValue{
        fr="En Ligne",
        en="Online",
        es="En Línea",
        it="Online",
        de="Online",
        pt="Online"
        }
    },
    {
    "tuto", new LocalizedValue{
        fr="Tutoriel",
        en="Tutorial",
        es="Tutorial",
        it="Tutorial",
        de="Tutorial",
        pt="Tutorial"
        }
    },
    {
    "tutoText", new LocalizedValue{
        fr=RichTextTranslations.tutoTextFr,
        en=RichTextTranslations.tutoTextEn,
        es=RichTextTranslations.tutoTextEs,
        it=RichTextTranslations.tutoTextIt,
        de=RichTextTranslations.tutoTextDe,
        pt=RichTextTranslations.tutoTextPt
        }
    },
    {
    "back", new LocalizedValue{
    fr="Retour",
    en="Back",
    es="Atrás",
    it="Indietro",
    de="Zurück",
    pt="Voltar"
        }
    },
    {
    "quit", new LocalizedValue{
        fr="Sortir",
        en="Exit",
        es="Salir",
        it="Esci",
        de="Beenden",
        pt="Sair"
        }
    },
    {
    "lobbyTitle", new LocalizedValue{
        fr="En Ligne",
        en="Online",
        es="En Línea",
        it="Online",
        de="Online",
        pt="Online"
        }
    },
    {
    "helperCreate", new LocalizedValue{
        fr="Pour créer une partie qu'un autre joueur pourra rejoindre, merci de renseigner son nom et de le partager.",
        en="To create a room for another player to join you, please enter a room name and share it.",
        es="Para crear una sala para que otro jugador se una, ingresa un nombre de sala y compártelo.",
        it="Per creare una stanza a cui un altro giocatore può unirsi, inserisci un nome stanza e condividilo.",
        de="Um einen Raum für einen anderen Spieler zu erstellen, gib einen Raumnamen ein und teile ihn.",
        pt="Para criar uma sala para outro jogador entrar, insira um nome para a sala e compartilhe."
        }
    },
    {
    "createButton", new LocalizedValue{
        fr="Créer",
        en="Create",
        es="Crear",
        it="Crea",
        de="Erstellen",
        pt="Criar"
        }
    },
    {
    "createPlaceHolder", new LocalizedValue{
        fr="partie ...",
        en="room ...",
        es="sala ...",
        it="stanza ...",
        de="Raum ...",
        pt="sala ..."
        }
    },
    {
    "helperJoin", new LocalizedValue{
        fr="Pour rejoindre une partie déjà créée, entrer son nom.",
        en="To join a created room please enter its room name.",
        es="Para unirte a una sala creada, ingresa el nombre de la sala.",
        it="Per unirti a una stanza creata, inserisci il nome della stanza.",
        de="Um einem erstellten Raum beizutreten, gib seinen Raumnamen ein.",
        pt="Para entrar em uma sala criada, insira o nome da sala."
        }
    },
    {
    "joinButton", new LocalizedValue{
        fr="Rejoindre",
        en="Join",
        es="Unirse",
        it="Unisciti",
        de="Beitreten",
        pt="Entrar"
        }
    },
    {
    "joinPlaceHolder", new LocalizedValue{
        fr="partie ...",
        en="room ...",
        es="sala ...",
        it="stanza ...",
        de="Raum ...",
        pt="sala ..."
        }
    },
    {
    "backButton", new LocalizedValue{
        fr="Retour",
        en="Back",
        es="Volver",
        it="Indietro",
        de="Zurück",
        pt="Voltar"
        }
    },
    {
    "loading", new LocalizedValue{
        fr="CHARGEMENT...",
        en="LOADING...",
        es="CARGANDO...",
        it="CARICAMENTO...",
        de="LADEN...",
        pt="CARREGANDO..."
        }
    },
    {
    "errorCreate", new LocalizedValue{
        fr="Vous devez renseigner un nom de partie pour créer une partie.",
        en="You must enter a room name to create a room.",
        es="Debes ingresar un nombre de sala para crear una partida.",
        it="Devi inserire il nome della stanza per creare una stanza.",
        de="Du musst einen Raumnamen eingeben, um einen Raum zu erstellen.",
        pt="Você deve inserir um nome de sala para criar uma sala."
        }
    },
    {
    "errorJoin", new LocalizedValue{
        fr="Vous devez renseigner un nom de partie pour rejoindre une partie.",
        en="You must enter a room name to join a room.",
        es="Debes ingresar un nombre de sala para unirte a una partida.",
        it="Devi inserire il nome della stanza per unirti a una stanza.",
        de="Du musst einen Raumnamen eingeben, um einem Raum beizutreten.",
        pt="Você deve inserir um nome de sala para entrar em uma sala."
        }
    },
    {
    "blueWin", new LocalizedValue{
        fr="Les Bleus\n gagnent !",
        en="The Blue\n Win !",
        es="¡Los Azules\n ganan!",
        it="I Blu\n Vincono!",
        de="Die Blauen\n gewinnen!",
        pt="Os Azuis\n Vencem!"
        }
    },
    {
    "whiteWin", new LocalizedValue{
        fr="Les Blancs\n gagnent !",
        en="The White\n Win !",
        es="¡Los Blancos\n ganan!",
        it="I Bianchi\n Vincono!",
        de="Die Weißen\n gewinnen!",
        pt="Os Brancos\n Vencem!"
        }
    },
    {
    "win", new LocalizedValue{
        fr="Vous avez Gagné !",
        en="You Win !",
        es="¡Has Ganado!",
        it="Hai Vinto!",
        de="Du hast gewonnen!",
        pt="Você Venceu!"
        }
    },
    {
    "lose", new LocalizedValue{
        fr="Vous avez Perdu :(",
        en="You Lose :(",
        es="¡Has Perdido :(",
        it="Hai Perso :(",
        de="Du hast verloren :(",
        pt="Você Perdeu :("
        }
    },
    {
    "waitingRoom", new LocalizedValue{
        fr="En attente de l'autre joueur.",
        en="Waiting for the other player.",
        es="Esperando al otro jugador.",
        it="In attesa dell'altro giocatore.",
        de="Warten auf den anderen Spieler.",
        pt="Aguardando o outro jogador."
        }
    },
    {
    "disconnectedRoom", new LocalizedValue{
        fr="L'autre joueur a quitté la partie.",
        en="The other player has left the game.",
        es="El otro jugador ha abandonado el juego.",
        it="L'altro giocatore ha abbandonato il gioco.",
        de="Der andere Spieler hat das Spiel verlassen.",
        pt="O outro jogador saiu do jogo."
        }
    },
    {
    "resumeButton", new LocalizedValue{
        fr="Reprendre",
        en="Resume",
        es="Continuar",
        it="Riprendi",
        de="Fortsetzen",
        pt="Continuar"
    }},
    };
    private string missingTextString = "Localized text not found";

    void Start()
    {
        Localize(LanguageSelector.selectedLang);
    }

    public void Localize(string lang)
    {
        if (credits != null) credits.text = GetLocalizedValue("credits", lang);
        if (start != null) start.text = GetLocalizedValue("start", lang);
        if (online != null) online.text = GetLocalizedValue("online", lang);
        if (tuto != null) tuto.text = GetLocalizedValue("tuto", lang);
        if (tutoText != null) tutoText.text = GetLocalizedValue("tutoText", lang);
        if (back != null) back.text = GetLocalizedValue("back", lang);
        if (quit != null) quit.text = GetLocalizedValue("quit", lang);

        if (loading != null) loading.text = GetLocalizedValue("loading", lang);

        if (lobbyTitle != null) lobbyTitle.text = GetLocalizedValue("lobbyTitle", lang);
        if (helperCreate != null) helperCreate.text = GetLocalizedValue("helperCreate", lang);
        if (createButton != null) createButton.text = GetLocalizedValue("createButton", lang);
        if (createPlaceHolder != null) createPlaceHolder.text = GetLocalizedValue("createPlaceHolder", lang);
        if (helperJoin != null) helperJoin.text = GetLocalizedValue("helperJoin", lang);
        if (joinButton != null) joinButton.text = GetLocalizedValue("joinButton", lang);
        if (joinPlaceHolder != null) joinPlaceHolder.text = GetLocalizedValue("joinPlaceHolder", lang);
        if (backButton != null) backButton.text = GetLocalizedValue("backButton", lang);

        if (resumeButton != null) resumeButton.text = GetLocalizedValue("resumeButton", lang);
        if (waitingRoom != null) waitingRoom.text = GetLocalizedValue("waitingRoom", lang);
        if (disconnectedRoom != null) disconnectedRoom.text = GetLocalizedValue("disconnectedRoom", lang);
        if (quitButtonPause != null) quitButtonPause.text = GetLocalizedValue("quit", lang);
        if (quitButtonWin != null) quitButtonWin.text = GetLocalizedValue("quit", lang);
        if (quitButtonDisconnected != null) quitButtonDisconnected.text = GetLocalizedValue("quit", lang);
    }

    public string GetLocalizedValue(string key, string language)
    {
        string result = missingTextString;
        if (localizedData.ContainsKey(key))
        {
            switch (language)
            {
                case "fr":
                    result = localizedData[key].fr;
                    break;
                case "en":
                    result = localizedData[key].en;
                    break;
                case "es":
                    result = localizedData[key].es;
                    break;
                case "it":
                    result = localizedData[key].it;
                    break;
                case "de":
                    result = localizedData[key].de;
                    break;
                case "pt":
                    result = localizedData[key].pt;
                    break;
            }
        }

        return result;
    }
}

[System.Serializable]
public class LocalizedItem
{
    public string key;
    public LocalizedValue value;
}

[System.Serializable]
public class LocalizedValue
{
    public string fr;
    public string en;
    public string es;
    public string it;
    public string de;
    public string pt;
}

[System.Serializable]
public class RichTextTranslations
{
    static public string tutoTextFr = "<b><color=#697498>Bienvenue dans DÉLEMENTS</color></b>\n" +
        "Dans DÉLEMENTS, chaque joueur a 8 dés. Chaque dé a trois symboles :\n" +
        "l'eau, le feu et la feuille.\n" +
        "\n<b><color=#697498>Les bases du jeu</color></b>\n" +
        "A chaque tour, un joueur choisit un dé à déplacer." +
        " Un dé peut être déplacé d'une ou deux cases." +
        " Le symbole visible sur le dé change à chaque déplacement.\n" +
        "\n<b><color=#697498>Engagement au combat</color></b>\n" +
        "Lorsqu'un dé termine son mouvement à côté d'un dé adverse, une confrontation a lieu." +
        "L'eau bat le feu, le feu bat la feuille, et la feuille bat l'eau. " +
        "Le dé victorieux prend la place du dé vaincu.\n" +
        "\n<b><color=#697498>La transformation en éclair</color></b>\n" +
        "Un dé peut devenir un éclair s'il atteint l'extrémité du plateau de jeu. " +
        "Un éclair peut vaincre tous les autres symboles, mais peut aussi être vaincu par n'importe quel autre symbole.\n" +
        "\n<b><color=#697498>Objectif du jeu</color></b>\n" +
        "Le but du jeu est de vaincre tous les dés de l'adversaire.";

    static public string tutoTextEn = "<b><color=#697498>Welcome to DÉLEMENTS</color></b>\n" +
        "In DÉLEMENTS, each player has 8 dice. Each dice has three symbols:\n" +
        "water, fire, and leaf.\n" +
        "\n<b><color=#697498>Game Basics</color></b>\n" +
        "On each turn, a player chooses a dice to move." +
        " A dice can be moved one or two squares." +
        " The visible symbol on the dice changes with each move.\n" +
        "\n<b><color=#697498>Engaging in Battle</color></b>\n" +
        "When a dice ends its move next to an opponent's dice, a confrontation occurs. " +
        "Water beats fire, fire beats leaf, and leaf beats water. " +
        "The victorious dice takes the place of the vanquished dice.\n" +
        "\n<b><color=#697498>The Lightning Transformation</color></b>\n" +
        "A dice can become lightning if it reaches the end of the game board. " +
        "Lightning can defeat all other symbols, but can also be defeated by any other symbol.\n" +
        "\n<b><color=#697498>Objective of the Game</color></b>\n" +
        "The goal of the game is to defeat all of the opponent's dice.";

    static public string tutoTextEs = "<b><color=#697498>Bienvenido a DÉLEMENTS</color></b>\n" +
        "En DÉLEMENTS, cada jugador tiene 8 dados. Cada dado tiene tres símbolos:\n" +
        "agua, fuego y hoja.\n" +
        "\n<b><color=#697498>Conceptos básicos del juego</color></b>\n" +
        "En cada turno, un jugador elige un dado para mover." +
        " Un dado puede moverse una o dos casillas." +
        " El símbolo visible en el dado cambia con cada movimiento.\n" +
        "\n<b><color=#697498>Enfrentamientos en combate</color></b>\n" +
        "Cuando un dado termina su movimiento junto a un dado del oponente, ocurre un enfrentamiento." +
        "El agua vence al fuego, el fuego vence a la hoja y la hoja vence al agua. " +
        "El dado victorioso ocupa el lugar del dado derrotado.\n" +
        "\n<b><color=#697498>Transformación en relámpago</color></b>\n" +
        "Un dado puede convertirse en relámpago si llega al extremo del tablero de juego. " +
        "El relámpago puede vencer a todos los demás símbolos, pero también puede ser vencido por cualquier otro símbolo.\n" +
        "\n<b><color=#697498>Objetivo del juego</color></b>\n" +
        "El objetivo del juego es vencer a todos los dados del oponente.";

    static public string tutoTextIt = "<b><color=#697498>Benvenuto in DÉLEMENTS</color></b>\n" +
        "In DÉLEMENTS, ogni giocatore ha 8 dadi. Ogni dado ha tre simboli:\n" +
        "acqua, fuoco e foglia.\n" +
        "\n<b><color=#697498>Basi del gioco</color></b>\n" +
        "Ad ogni turno, un giocatore sceglie un dado da muovere." +
        " Un dado può essere mosso di una o due caselle." +
        " Il simbolo visibile sul dado cambia ad ogni mossa.\n" +
        "\n<b><color=#697498>Impegnarsi in battaglia</color></b>\n" +
        "Quando un dado termina il suo movimento accanto a un dado avversario, avviene un confronto. " +
        "L'acqua batte il fuoco, il fuoco batte la foglia e la foglia batte l'acqua. " +
        "Il dado vittorioso prende il posto del dado sconfitto.\n" +
        "\n<b><color=#697498>La trasformazione in fulmine</color></b>\n" +
        "Un dado può diventare un fulmine se raggiunge la fine della scacchiera di gioco. " +
        "Il fulmine può sconfiggere tutti gli altri simboli, ma può anche essere sconfitto da qualsiasi altro simbolo.\n" +
        "\n<b><color=#697498>Obiettivo del gioco</color></b>\n" +
        "Lo scopo del gioco è sconfiggere tutti i dadi dell'avversario.";

    static public string tutoTextDe = "<b><color=#697498>Willkommen bei DÉLEMENTS</color></b>\n" +
        "In DÉLEMENTS hat jeder Spieler 8 Würfel. Jeder Würfel hat drei Symbole:\n" +
        "Wasser, Feuer und Blatt.\n" +
        "\n<b><color=#697498>Grundlagen des Spiels</color></b>\n" +
        "In jedem Zug wählt ein Spieler einen Würfel zum Bewegen." +
        " Ein Würfel kann ein oder zwei Felder bewegt werden." +
        " Das sichtbare Symbol auf dem Würfel ändert sich bei jedem Zug.\n" +
        "\n<b><color=#697498>Kämpfe austragen</color></b>\n" +
        "Wenn ein Würfel sein Bewegungsende neben einem gegnerischen Würfel hat, kommt es zu einer Konfrontation." +
        "Wasser schlägt Feuer, Feuer schlägt Blatt und Blatt schlägt Wasser. " +
        "Der siegreiche Würfel nimmt den Platz des besiegten Würfels ein.\n" +
        "\n<b><color=#697498>Die Blitz-Transformation</color></b>\n" +
        "Ein Würfel kann sich in einen Blitz verwandeln, wenn er das Ende des Spielfelds erreicht. " +
        "Ein Blitz kann alle anderen Symbole besiegen, kann aber auch von jedem anderen Symbol besiegt werden.\n" +
        "\n<b><color=#697498>Ziel des Spiels</color></b>\n" +
        "Das Ziel des Spiels ist es, alle Würfel des Gegners zu besiegen.";

    static public string tutoTextPt = "<b><color=#697498>Bem-vindo ao DÉLEMENTS</color></b>\n" +
        "Em DÉLEMENTS, cada jogador tem 8 dados. Cada dado tem três símbolos:\n" +
        "água, fogo e folha.\n" +
        "\n<b><color=#697498>Noções básicas do jogo</color></b>\n" +
        "Em cada turno, um jogador escolhe um dado para mover." +
        " Um dado pode ser movido uma ou duas casas." +
        " O símbolo visível no dado muda a cada movimento.\n" +
        "\n<b><color=#697498>Envolvendo-se em batalha</color></b>\n" +
        "Quando um dado termina seu movimento ao lado de um dado do oponente, ocorre um confronto." +
        "Água vence fogo, fogo vence folha e folha vence água. " +
        "O dado vitorioso ocupa o lugar do dado derrotado.\n" +
        "\n<b><color=#697498>A Transformação Relâmpago</color></b>\n" +
        "Um dado pode se transformar em relâmpago se alcançar o final do tabuleiro de jogo. " +
        "O relâmpago pode vencer todos os outros símbolos, mas também pode ser derrotado por qualquer outro símbolo.\n" +
        "\n<b><color=#697498>Objetivo do jogo</color></b>\n" +
        "O objetivo do jogo é vencer todos os dados do oponente.";
}