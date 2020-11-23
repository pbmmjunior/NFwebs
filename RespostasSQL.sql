--A) Escreva uma instru��o SQL que insira 1 (um) registro na tabela Store.

INSERT INTO STORE (STOR_ID, STOR_NAME) VALUES (1, 'LOJA');

--B) Escreva uma instru��o SQL que atualize a informa��o do campo type para �culin�ria� da tabela Titles onde o c�digo do t�tulo deve ser igual a �MC3021�.

UPDATE TITLES SET TYPE = 'culin�ria' WHERE TITLE = 'MC3021';

--C) Escreva uma instru��o SQL que delete todos os registros da tabela Sales onde o c�digo da loja deva ser igual a �7066�.

DELETE FROM SALES WHERE STOR_ID = '7066'

--D) Escreva uma instru��o SQL que retorne somente as lojas que n�o possuem vendas.

SELECT * FROM STORE ST WHERE NOT EXISTS (SELECT * FROM SALES SL WHERE ST.STOR_ID = SL.STOR_ID);

--E) Escreva uma instru��o SQL que retorne somente os t�tulos que n�o foram vendidos.

SELECT * FROM TITLES T WHERE NOT EXISTS (SELECT * FROM SALES S WHERE T.TITLE_ID = S.TITLE_ID);

--F) Escreva uma instru��o SQL que retorne os t�tulos e a somat�ria da quantidade de t�tulos vendidos.

SELECT T.TITLE, SUM(S.QTY) AS SOMAT�RIO
  FROM TITLES T INNER JOIN SALES S
    ON T.TITLE_ID = S.TITLE_ID
 GROUP BY T.TITLE

--G) Escreva uma instru��o SQL que retorne os registros da tabela Titles que tenham mais que 2 ocorr�ncias com o mesmo t�tulo.

SELECT T.TITLE, COUNT(*)
  FROM TITLES T
 GROUP BY T.TITLE
HAVING COUNT(*) > 2