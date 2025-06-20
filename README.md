# EmailOrderApp

Aplikacja do automatycznego pobierania zamówień z wiadomości e-mail, przetwarzania ich za pomocą AI (Gemini API) i wyświetlania w panelu Blazor Server.

---

## Funkcje

- Pobieranie maili z IMAP
- Przetwarzanie treści na encje zamówień przez AI (Gemini)
- Zapisywanie danych do MySQL
- Interaktywny panel Blazor Server z Radzen
- Sortowanie i zaawansowane filtrowanie wyników

## Wymagania

- Docker
- Konto e-mail z IMAP
- Klucz API Gemini (Google AI)
- Plik `.env` z konfiguracją (repozytorium zawiera plik `.env.example` ze strukturą)

## Uruchamianie aplikacji

- Utwórz plik `.env` w katalogu głównym i uzupełnij jego zawartość zgodnie z `.env.example`
- W terminalu wpisz `docker compose up --build` (migracje wykonają się automatycznie)
- Aplikacja będzie dostępna pod adresem `http://localhost:8080`
