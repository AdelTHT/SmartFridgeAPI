SmartFridgeApi
SmartFridgeApi est une API RESTful ASP.NET Core permettant de gérer vos ingrédients et vos recettes de cuisine de façon sécurisée avec une authentification JWT.
Le projet inclut un backend organisé proprement, et un frontend HTML/CSS moderne pour une démonstration simple et efficace.

🚀 Fonctionnalités
Authentification JWT (inscription, connexion)

Gestion des utilisateurs, ingrédients, recettes

Endpoints CRUD pour ingrédients et recettes

Documentation Swagger interactive et personnalisée

Validation des entrées (annotations, erreurs claires)

Frontend responsive (HTML/CSS/JS) pour tester facilement toutes les fonctionnalités

📦 Démarrage rapide
Prérequis
.NET 8 SDK

Navigateur web récent

Lancer le backend
bash
Copier le code
dotnet restore
dotnet run
L’API sera accessible sur http://localhost:5027
Swagger (documentation interactive) : http://localhost:5027/swagger

Lancer le frontend
Ouvre simplement le fichier index.html dans ton navigateur (double-clique ou glisse sur une fenêtre).

🔑 Authentification
Inscription : /api/Auth/register (POST) avec { "email": "...", "password": "..." }

Connexion : /api/Auth/login (POST), retourne un token JWT

Tous les endpoints protégés nécessitent le header HTTP :

makefile
Copier le code
Authorization: Bearer <votre_token>
Identifiants de test recommandés
Créer le compte 

🛠️ Endpoints principaux
Méthode	Route	Description	Auth ?
POST	/api/Auth/register	Inscription	Non
POST	/api/Auth/login	Connexion	Non
GET	/api/Ingredients	Liste ingrédients	Oui
POST	/api/Ingredients	Ajouter ingrédient	Oui
PUT	/api/Ingredients/{id}	Modifier ingrédient	Oui
DELETE	/api/Ingredients/{id}	Supprimer ingrédient	Oui
GET	/api/Recipes	Liste recettes	Oui
POST	/api/Recipes	Ajouter recette	Oui
DELETE	/api/Recipes/{id}	Supprimer recette	Oui
GET	/api/Auth/users	Voir tous les utilisateurs	Oui (admin)
DELETE	/api/Auth/users/{id}	Supprimer un user	Oui (admin)

Documentation Swagger générée automatiquement pour chaque endpoint.

🌐 Démo Frontend
Ouvre le fichier index.html

Permet :

de s’inscrire/se connecter

de gérer les ingrédients (ajout, modification, suppression)

d’ajouter des recettes (avec sélection d’ingrédients)

de consulter toutes les recettes et voir les ingrédients associés

Design moderne, adaptatif mobile & desktop

📁 Structure du projet
<pre> ```plaintext SmartFridgeApi/ ├── Controllers/ │ ├── AuthController.cs │ ├── IngredientsController.cs │ └── RecipesController.cs ├── Data/ │ └── SmartFridgeDbContext.cs ├── DTOs/ │ ├── CreateIngredientDto.cs │ ├── UpdateIngredientDto.cs │ ├── IngredientDto.cs │ ├── CreateRecipeDto.cs │ ├── RecipeDto.cs │ ├── RegisterDto.cs │ └── LoginDto.cs ├── Models/ │ ├── Ingredient.cs │ ├── Recipe.cs │ ├── RecipeIngredient.cs │ └── User.cs ├── Services/ │ ├── AuthService.cs │ ├── IngredientService.cs │ └── RecipeService.cs ├── index.html ├── Program.cs └── README.md ``` </pre>

SmartFridgeApi/
      
      ├── Controllers/
      │    ├── AuthController.cs
      │    ├── IngredientsController.cs
      │    └── RecipesController.cs
      │
      
      ├── Data/
      │    └── SmartFridgeDbContext.cs
      │
      ├── DTOs/
      │    ├── CreateIngredientDto.cs
      │    ├── UpdateIngredientDto.cs
      │    ├── IngredientDto.cs
      │    ├── CreateRecipeDto.cs
      │    ├── RecipeDto.cs
      │    ├── RegisterDto.cs
      │    └── LoginDto.cs
      │
      ├── Models/
      │    ├── Ingredient.cs
      │    ├── Recipe.cs
      │    ├── RecipeIngredient.cs
      │    └── User.cs
      │
      ├── Services/
      │    ├── AuthService.cs
      │    ├── IngredientService.cs
      │    └── RecipeService.cs
      │
      ├── index.html
      ├── Program.cs
      └── README.md

👨‍💻 Auteurs
Adel TAHANOUT

Projet réalisé dans le cadre de l’évaluation WebAPI SIO 2025
