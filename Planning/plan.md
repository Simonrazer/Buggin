# Its Buggin Time
Man ist ein Scentient Computer Bug, Ziel = Überleben/Weltherrschaft

## Umgebung
Speicherbereich, der eigene ist hell, der ist sicher, anfangs sehr wenig. Man kann ins dunkle laufen und erleuchtet diesen so schwach, wodurch Fallen/Hürden/Ressourcen aufgedeckt werden die man zerstören/abbauen kann. 
Um Bereiche hell bleiben zu lassen muss man Lichter aufstellen, diese gehen nach einer Zeit wieder aus wenn man sie nicht wieder auffüllt.
Wenn man sie über Kabel zu seinem Speicherbereich verbindet leuchten sie ewig. Kabel können von Antiviren angegriffen werden.
Bugs die man unter seiner Kontrolle hat nehmen Schaden/sterben dunklen Bereichen, da sie reallokiert werden.
Genauso wie alle "Gebäude" dort. Auch spawnen dort wieder die Ursprünglichen Bewohner.

Im Zentrum vom Startbereich ist das eigene Programm, an den die Kabel angeschlossen werden.
Dieses ist es auch wenn man an sich selbst upgrades durchführt.
Wenn dieser zerstört wird stirbt man. Wenn die Spielerfigur stirbt wird man dorthin zurückgesetzt und alles was man dabei hatte ist gone.

## Sachen
- Hindernisse
Feste Wände im Speicherbereich, können nicht kaputt gemacht werden.
- Fallen
Manche Dinge im unerleuchteten Speicherbereich sind unfreundlich (Spikes, Löcher, Gegner)
- Kabel
Verbinden Gebäude die Energie brauchen. Können von Fallen/Antiviren kaputt gemacht werden
- Lampen
Erleuchten neuen Speicherbereich, sodass man seine Bugs dort verwenden kann. Verhindert das neue Wesen spawnen (low level). Brauchen Kabelanschluss oder verbrennen Bugs zum laufen.
Upgrades: mehr Radius, längere Laufzeit ohne nachfüllung, weniger Bugs verwenden.
Augments: Throwable, Toggleable, Moveable, Following, Retractable
- Wände
Können nicht von Fallen passiert werden. Antiviren können sie angreifen/zerstören und in so in den eigenen Speicherbereich eindringen.
Upgrades: eingebaute Lampen die nach innen und außen gehen, stabiler
- Antiviren
Patroulieren überall, unterschiedliche stärken je nach Gebiet. Wenn sie auf Licht treffen greifen Sie dort alles an.
- Reallokierung
Lässt alles außer man selbst in dunklen Speicherbereichen schaden nehmen. Fallen/Ressourcen spawnen je nach Biom wieder dort.
- Karten
Hindernisslayout von einem Teil des Speicherbereichs, als Hilfe zur erkundung. Schon platzierte Gebäude sind dort zu sehen.
- Eigenes Programm
Dort gibt man Ressourcen ab um sie nicht draußen zu verlieren (Manifeste/Freie Bits). Und hier werden große Gebäude gewählt zum bauen.

## Ressourcen
- Bugs (Farmable)
Drops von Fallen, Antiviren. Random schwache Programme im unbekannten Bereich die man töten kann.
Erteilen von leichten Aufgaben/als Waffe.
- Freie Bits (Farmable)
Getötete Gegner zerfallen zu freien Bits.
Können genutzt werden um Gebäude zu bauen.
- Libraries
Werden an Orten im Speicherbereich gefunden.
Schalten spezifische Sachen frei, welche ist Playerwahl beim einsammeln (Upgradetree).
Verschiedene Arten Libraries/Upgradetrees für verscheidene Sachen?
- Coroutines
Drops von High Level dudes.
Schalten neue Player fähigkeiten frei.
- Manifests (Farmable in high Level areas)
Werden für high level Gebäude gebraucht, wird bei Bau verbraucht.
- Admin Rechte
Werden von High Level Gegnern gedroppt. Schalten advanceten Stuff frei (Schalten Pfade im Upgradetree und neue Gebäude frei).

## Bugs
Kleine schwarze Blobs die rumhüpfen. Man Wählt ein paar von ihnen mit einem Auswahlrechteck (maus) aus und gibt diesen dann Befehle.
Befehle:
- Gebäude betreiben
Sie warten an diesem und hüpfen rein wenn es mehr zum laufen braucht. Verbrauchte sind weg.
- Folge mir
- Angriff
    - Basic attack: Bugs schmeisen sich in Gegner. 80% Das es zurück kommt.
    - Siehe nächstes

## Was ist der Player
Ein größerer Blob der sich bewegen kann, angreifen kann und mit Objekten interagieren.
Corountines sind Upgradepunkte auf self upgradetree.
Auf diesem Tree sind spezielle eigene Angriffe und extra Sachen die man mit den Bugs machen kann.
Bsp:
- Slice: Bugs formen eine Klinge. 90° Angriffswinkel, wenig range, 70% kommen zurück.
- Swarm: Bugs formen Kreis um Gegner und prasseln drauf ein. 80% kommen zurück.
- long Stab: Bugs formen Schwert nach vorne, 1° Angriffswinkel, mehr range, 60% kommen zurück.
- Heal: Bugs fliegen in Player und heilen ihn. Nix kommt zurück.
Er kann auch dashen I guess

## Misc Gebäude
- Teleportpunkte
Können Spieler+Bugs zu anderen TP Punkten TPen. Braucht Kabelanschluss.
- Katapult
Schmeißt Bugs auf angreifende Antiviren in einem Bereich. Braucht Kabel + Bugs als Munnition.
- Overseer
Kontrolliert die Bugs in Radius sodass sie Antiviren angreifen. Können nur Basic attacks.

## Gameplayloop
Man erkundet neue Bereiche indem man rausläuft und temporäre Lampen platziert. Wenn man auf Gegner trifft bekämpft man sie selbst oder mit Hilfe seiner Bugs, wobei man für sie Licht schaffen muss. 
Man findet Loot (Bits, Bugs, Libraries) mit denen man Sachen bauen kann, vorallem seine Basis besser schützen.
Diese wird immer wieder von Antiviren angegriffen, umso mehr Licht man erzeugt um so mehr Wesen kommen.
Um noch bessere Verteidigungen zu bauen braucht man Ressourcen, welche man aus wieder abgedunkelten Bereichen farmen kann.
Higher Level Bereiche geben mehr Loot.
Um Bessere Sachen freizuschalten muss man Libraries/Adminrechte/Coroutinen finden. Diese werden auf gefundenen Karten geteasert. Dort sind Bosskampfsachen.

## Controlls
Player läuft mit WASD. Dash mit Shift. Seine Attack ist mit Maus1. Bugs auswählen ist mit Maus2, Modifier für Menge = Ctrl. 
Unten ist eine Minecraft like Hotbar für Aktionen, die selber belegbar sind. Aber nur 4 Solts. Dazu zählt kleine Sachen bauen (lamps, kabel, wand) und Bug Special Attacks.
Tab = Bauaktionenmenue
E = Kampfaktionenmenue
Bugs erteilt man simple Aufgaben indem man auf die Entsprechende Sache Maus2 klickt.