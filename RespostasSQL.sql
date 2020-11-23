--A) Escreva uma instrução SQL que insira 1 (um) registro na tabela Store.

INSERT INTO STORE (STOR_ID, STOR_NAME) VALUES (1, 'LOJA');

--B) Escreva uma instrução SQL que atualize a informação do campo type para “culinária” da tabela Titles onde o código do título deve ser igual a “MC3021”.

UPDATE TITLES SET TYPE = 'culinária' WHERE TITLE = 'MC3021';

--C) Escreva uma instrução SQL que delete todos os registros da tabela Sales onde o código da loja deva ser igual a “7066”.

DELETE FROM SALES WHERE STOR_ID = '7066'

--D) Escreva uma instrução SQL que retorne somente as lojas que não possuem vendas.

SELECT * FROM STORE ST WHERE NOT EXISTS (SELECT * FROM SALES SL WHERE ST.STOR_ID = SL.STOR_ID);

--E) Escreva uma instrução SQL que retorne somente os títulos que não foram vendidos.

SELECT * FROM TITLES T WHERE NOT EXISTS (SELECT * FROM SALES S WHERE T.TITLE_ID = S.TITLE_ID);

--F) Escreva uma instrução SQL que retorne os títulos e a somatória da quantidade de títulos vendidos.

SELECT T.TITLE, SUM(S.QTY) AS SOMATÓRIO
  FROM TITLES T INNER JOIN SALES S
    ON T.TITLE_ID = S.TITLE_ID
 GROUP BY T.TITLE

--G) Escreva uma instrução SQL que retorne os registros da tabela Titles que tenham mais que 2 ocorrências com o mesmo título.

SELECT T.TITLE, COUNT(*)
  FROM TITLES T
 GROUP BY T.TITLE
HAVING COUNT(*) > 2