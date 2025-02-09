# OHVTrainer
Trainer jest tworzony powoli, gdyż głównym moim priorytetem jest aktualnie tworzenie własnej gry.

### Instalacja
Jeśli instalujesz trainer **pierwszy raz**, kliknij tutaj aby pobrać pełny instalator.
Jeśli posiadasz już trainer, ale **chcesz go zaktualizować**, kliknij tutaj aby pobrać tylko trainer.

Pobrane archiwum wypakuj do folderu z grą np. `C:\Program Files (x86)\Steam\steamapps\common\OHV`, uruchom grę, wczytaj zapis, a po jego wczytaniu, **wciśnij F5 aby wyświetlić okno trainera**.

### Tworzenie Własnych Modów
Niestety, nie mam opcji stworzenia menedżera módow - taki menedżer wymagałby utworzenie serwera aby owe modyfikacje tam umieszczać - jednak instalacja manualna modyfikacji utworzonych na bazie [BepInEx](https://github.com/BepInEx/BepInEx "BepInEx") jest banalnie prosta.

Stworzyłem też mały poradnik w jaki sposób tworzyć modyfikacje do OHV - jeśli masz doświadczenie w silniku Unity, tworzenie modyfikacji jest banalnie proste.

Wymagania (w kolejności):
- [.NET SDK](https://dotnet.microsoft.com/en-us/download)
- [Visual Studio Community 2022](https://visualstudio.microsoft.com/pl/vs/community/ "Visual Studio Community 2022")
- [Narzędzia BepInEx](https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.2/BepInEx_win_x64_5.4.23.2.zip "Narzędzia BepInEx") **(Wypakuj do folderu z grą)**
- [dnSpy](https://github.com/dnSpy/dnSpy/releases/download/v6.1.8/dnSpy-net-win64.zip) - wypakuj to gdziekolwiek chcesz - jest narzędzie pozwalające na dekompilacje kodu gry oraz jego przeglądanie/twardą modyfikacje (jeśli umiesz angielski, to się odnajdziesz).

Po wykonaniu czynności napisanych wyżej, uruchom grę do menu głównego i ją wyłącz aby BepInEx wygenerował pliki konfiguracyjne.
Następnie w **[FolderGry]/BepInEx/config/BepInEx.cfg** znajdź tekst `[Logging.Console]` i ustaw pod nim `Enabled` na `true`.
Powinno to wyglądać mniej więcej tak:
```
[Logging.Console]
Enabled = true
```
Zmiana ta sprawi, że wraz z uruchomieniem gry, uruchomi się również konsola, do której możemy wpisywać tekst w ramach debugowania (konsola ta wyświetla też logi z uruchomionej gry Unity, można to jednak wyłączyć).

Teraz, uruchom **Wiersz Polecenia**, najlepiej **jako administrator**, wklej w nim i zatwierdź:
`dotnet new install BepInEx.Templates::2.0.0-be.4 --nuget-source https://nuget.bepinex.dev/v3/index.json`
Komenda ta pobierze szablony projektów Visual Studio do tworzenia modyfikacji dla BepInEx.

Ostatnim krokiem jest stworzenie samego projektu, aby to zrobić, wejdź do folderu, gdzie chcesz aby projekt został utworzony, następnie w pasku eksploratora plików, gdzie wyświetlana jest ścieżka do folderu, wpisz **CMD** - otworzy to Wiersz Poleceń właśnie w tym folderze.
W Wierszu Poleceń wklej tą komendę:
`dotnet new bepinex5plugin -n NazwaPluginu -T netstandard2.1 -U 2021.2.9`
Następnie zamień *NazwaPluginu* na swoją nazwę modyfikacji np. OHVFlashlight, FlashlightMod i zatwierdź komendę wciskając **Enter**. 
W folderze o którym pisaliśmy wcześniej, pojawi się nowy folder z nazwą twojej modyfikacji, wejdź w niego i uruchom w Visual Studio plik o nazwie **[*NazwaPluginu*.csproj]**. 
Po uruchomieniu odczekaj aż Visual Studio skończy rozwiązywanie projektu i spróbuj zamknąć Visual Studio - pojawi się komunikat czy chcesz zapisać plik *.sln*. kliknij **tak** i zapisz w tym samym folderze gdzie znajduje się **[*NazwaProjektu*.csproj]**. Po zamknięciu Visual Studio, włącz go jeszcze raz, ale tym razem poprzez plik **[*NazwaProjektu*.sln]**.
Teraz możesz zacząć tworzyć swoją modyfikację!

Jeśli jednak pustym wzrokiem patrzysz się w ekran zastanawiając się "i co dalej?", przeczytaj co jeszcze *chyba* musisz zrobić:

1.  Zaimportuj skompilowany kod gry do projektu modyfikacji.
	Bez tego pliku nie wywołasz zmian takich jak np. nieskończona stamina/życie.
	Dla gier Unity jest to plik `Assembly-CSharp.dll`, który znajduje się w `[LokalizacjaGry]/OHV_Data/Managed`. Możesz go również przeglądać w dnSpy aby sprawdzać, w której klasie co robi jakaś funkcja - jeśli nie chcesz, nie musisz, ale nie będziesz wiedział dokładnie co robi któraś z wybranych przez ciebie funkcji w kodzie gry.
	
	Aby go zaimportować: W Visual Studio, po prawej stronie ekranu znajduję się okienko o nazwie `Eksplorator rozwiązań`, w nim jest przycisk z tekstem `Zależności` - kliknij na nim prawym przyciskiem myszy i kliknij `Dodaj odwołanie do projektu`. Otworzy się okno, w którym klikniesz `Przeglądaj...`, znajdź plik `Assembly-CSharp.dll` w miejscu gdzie napisałem wyżej, kliknij go dwa razy, a następnie potwierdź w poprzednim oknie jego dodanie klikając `OK`.
	
 [![Zależności](https://i.ibb.co/CpykXVC8/image.png "Zależności")](https://i.ibb.co/CpykXVC8/image.png "Zależności")

2. Dodatkowe biblioteki.
	Być może chcesz utworzyć funkcję, która będzie działać tylko jeśli gracz wciśnie jakiś przycisk, a kod, który pewnie znalazłeś na Google:
```
if (Input.GetKeyDown(KeyCode.F5))
{
    OHVEconomyManager.instance.AddCash(69666420);
}
```
nie działa? 
Pewnie dlatego bo gra korzysta z nowego Input System od Unity.
Musisz zrobić to samo co z plikiem `Assembly-CSharp.dll`, ale zamiast tego pliku, wybierz plik o nazwie `Unity.InputSystem.dll`.
Wtedy zamień poprzedni kod na:
```
if (Keyboard.current.f5Key.wasPressedThisFrame)
{
    OHVEconomyManager.instance.AddCash(69666420);
}
```

3. Chcę dodać UI lub model 3D do gry.
	Z modelem nie pomogę - próbowałem to zrobić, ale każdy model który wgrywałem do gry był przezroczysty :/
	Jeśli chcesz dodać UI (np. okno z trainerem) do gry, musisz zainstalować [Unity 3D w wersji **2021.2.9f1**](https://download.unity3d.com/download_unity/921b45a28ab6/Windows64EditorInstaller/UnitySetup64-2021.2.9f1.exe). 
	W nim tworzysz okno trainera, przeciągasz z go hierarchi  do eksploratora assetów w unity (aby zapisać go jako prefab), potem w oknie po prawej przypisujesz go do AssetBundle:
	[![AssetBundle settings](https://i.ibb.co/rKXT2ZkR/image.png "AssetBundle settings")](https://i.ibb.co/rKXT2ZkR/image.png "AssetBundle settings")
	No i co kurde teraz? Musisz Unity w folderze głównym Assets, musisz utworzyć nowy folder o nazwie Editor, w nim utwórz nowy skrypt o nazwie `AssetBundleExporter`. 
	Otwórz skrypt, usuń zawarty w nim tekst i wklej ten kod: https://pastebin.com/ZrUfBamK
	
	Zapisz skrypt **ctrl + s**, zamknij okno, poczekaj aż Unity 3D skompiluje nowy kod.
	Teraz kliknij przycisk pokazany na zdjęciu:
	[![](https://i.ibb.co/twZGn2n8/image.png)](https://i.ibb.co/twZGn2n8/image.png)
	Poczekaj chwilę, jeśli wszystko zrobiłeś poprawnie, w folderze AssetBundles powinieneś mieć plik o nazwie np. `ohvtrainer.trainer`.

W swoim pluginie ładujesz go tym kodem:
```
AssetBundle bundle = AssetBundle.LoadFromFile("ścieżka do AssetBundle np: C:\ohvtrainer.trainer");
if (bundle ==  null)
{
    Logger.LogError("Cannot load Trainer AssetBundle!");
    return;
}

GameObject prefab = bundle.LoadAsset<GameObject>("TrainerCanvas");

if (prefab == null)
{
    Logger.LogError("Cannot load \"TrainerCanvas\" from AssetBundle!");
    return;
}

trainerWindow = Instantiate(prefab);

if (trainerWindow == null)
{
    Logger.LogError("Cannot instantiate trainerWindow!");
}
```
Okno pojawi się zaraz po wczytaniu zapisu.
Bardzo prawdopodobne, że będziesz potrzebował również zaimportować bibliotekę `Unity.TextMeshPro.dll`, aby móc edytować lub czytać tekst z pól tekstowych w trainerze.

Więcej was nie nauczę bo już mi nie starcza cierpliwości :P
Możecie jeszcze zerknąć na kod mojego Trainera dla przykładów.
Życzę powodzenia!
