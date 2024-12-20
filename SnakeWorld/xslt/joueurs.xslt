<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:jeu="http://www.univ-grenoble-alpes.fr/l3miage/jeu">
    <xsl:output method="html"/>
    
    
    <xsl:template match="/">
        <html>
            <head>
                <meta charset="UTF-8"/>
                <title>Le score des Joueurs</title>
                <link rel="stylesheet" href="../css/Joueur.css"/>
            </head>
            <body>
                <div class ="cadre">
                    <div class="infos">
                        <h4>  Rang des joueurs selon leur meilleur score réalisé </h4>
                        <p>
                            Salut ! Tu n'as encore pas atteint le Top 1? Bah vas y !! <br/>Refais encore une partie !! <br/>
                            Si tu es le numero 1, assure toi d'être à l'abri en montant ton score, aller c'est parti !!
                        </p>
                        <table class ="tab">
                            <tr><th>Rang</th><th>Nom du Joueur</th> <th> Meilleur Score réalisé</th> </tr>
                            <xsl:apply-templates select="jeu:joueurs/jeu:joueur">
                                <xsl:sort select="jeu:meilleurScore" order="descending" data-type="number"/>
                            </xsl:apply-templates>
                        </table>
                    </div>
                    
                </div>
                
            </body>
        </html>
    </xsl:template>
    
    <!--- Le template pour récupere le nom et le score des joueus  -->
    
    <xsl:template match="jeu:joueur">
        <tr>
            <td>
              <xsl:value-of select="position()"/>  
            </td>
            <td>
                <xsl:value-of select="jeu:login"/>
            </td>
            <td>
                <xsl:value-of select="jeu:meilleurScore"/>
            </td>
        </tr>
    </xsl:template>
</xsl:stylesheet>