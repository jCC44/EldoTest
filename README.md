## Test-Algo

Vous trouverez dans les fichiers sources le projet Visual Studio 2022.
Les relations exemples utilisées sont comme ci dessous:
![Image text](/relation.png)

J'ai fais le choix d'une reconnaissance des prénoms via les majuscules. Il y avait aussi la possibilité de faire du parsing avec les syntaxes de phrases.
Dans votre énoncé, il est indiqué "Axiome: `l'ami de mon ami est mon ami`", je suis donc parti sur le principe que les amis des amis de mes amis sont mes amis... J'ai donc fait un parcours en largeur avec une récursion. 
Si les amis s'arrêtent dans un rayon de 2 nodes il est facilement possible d'ajouter une limite dans la récursion.



## Test-SQL

Voici la requète: 

``` sql
SELECT *
FROM artisan
         LEFT JOIN (
    SELECT
        *,
        ROW_NUMBER() OVER (
            PARTITION BY artisan_id
            ORDER BY review_date DESC
            ) AS row_num
    FROM user_review
) AS user_review ON user_review.artisan_id = artisan.id
WHERE row_num <= 3;
```

c'est la première fois que j'utilise ROW_NUMBER. J'ai trouvé la solution sur ce site: https://www.mysqltutorial.org/mysql-window-functions/mysql-row_number-function/