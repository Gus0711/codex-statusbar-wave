# Codex Statusbar Wave

Une barre de statistiques animée pour **Codex Desktop sur Windows**, avec un dégradé indigo, violet et bleu-vert qui se déplace comme une vague.

Variante communautaire de [dongyue-19/codex-statusbar](https://github.com/dongyue-19/codex-statusbar), basée sur la version **1.0.0-rc3**. Projet indépendant, non affilié à OpenAI.

## Installation rapide

1. Ouvrir la page [Releases](https://github.com/Gus0711/codex-statusbar-wave/releases/latest).
2. Télécharger **CodexStatusbarWave-win-x64.zip** dans les fichiers de la version.
3. Décompresser le ZIP dans un dossier conservé sur le PC, par exemple Documents\CodexStatusbarWave. Éviter un dossier synchronisé ou partagé : la configuration peut y être enregistrée.
4. Ouvrir Codex Desktop, puis double-cliquer sur **Lancer.cmd**.

Il s'agit d'une application portable : aucun droit administrateur, aucune installation de .NET et aucun redémarrage de Codex ne sont nécessaires. Utiliser le lanceur, qui charge la configuration fournie et désactive le démarrage automatique avec Windows. **Lancer directement l'EXE utilise les réglages du projet d'origine, qui peuvent activer ce démarrage automatique.**

La barre apparaît au-dessus de la saisie du chat. Elle suit le chat actif et se masque lorsque Codex passe en arrière-plan ou que la saisie est absente ou recouverte. Quitter depuis l'icône de Statusbar dans la zone de notification Windows. Une seule instance peut fonctionner à la fois : quitter une ancienne Statusbar avant de lancer celle-ci.

## Informations affichées

| Indication | Signification |
|---|---|
| ⚡ tok/s | Débit de sortie calculé à partir des événements de Codex ; inclut les tokens de raisonnement. |
| tok | Tokens cumulés du chat : entrées + sorties. Ce n'est pas la taille du contexte. |
| Cache | Part des tokens d'entrée issus du cache. |
| Ctx | Pourcentage de la fenêtre de contexte utilisé. |
| Hebdo | Quota hebdomadaire **restant**, selon le dernier relevé officiel du chat. |
| 5 h | Quota sur cinq heures restant, uniquement lorsque Codex le fournit. |

Les quotas se mettent à jour pendant les échanges. Ouvrir un ancien chat peut afficher un relevé ancien ; la barre n'interroge pas le compte sur Internet. Une fenêtre arrivée à expiration affiche « — » jusqu'au prochain relevé. Sur une fenêtre étroite, la barre réduit les informations en donnant priorité aux quotas et au contexte.

## Apparence

L'animation effectue un cycle d'environ neuf secondes à 30 images/s et s'arrête lorsque la barre est masquée. Le texte reste fixe. Les coins sont transparents et la barre laisse passer les clics.

Après avoir quitté Statusbar, modifier `settings.json`, puis relancer :

- `theme` : `Auto`, `Dark` ou `Light` pour le texte ; le fond animé reste sombre. Choisir `Dark` si le texte est trop sombre sur ce fond.
- `overlayScalePercent` : taille de 60 à 130, par défaut 110.
- `transparentBackground` : `true` enlève le fond et son animation.

## Compatibilité et confidentialité

Windows 10/11 **x64**, avec Codex Desktop lancé sous le même utilisateur. Testé dans Codex **26.1111.2228.0** ; la lecture des événements et la détection de la saisie dépendent de formats internes qui peuvent changer lors d'une mise à jour. Les autres versions ne sont pas garanties.

Le programme lit les fichiers de session, la base locale et les notifications IPC de Codex. Il utilise l'accessibilité Windows en lecture seule pour repérer la saisie. Il ne modifie pas l'application Codex et n'ajoute pas de requête réseau ni de télémétrie. Les fichiers de session ne sont pas modifiés.

Le lanceur fourni n'active pas les journaux de diagnostic. L'option `--debug` écrit un journal local dans `%LOCALAPPDATA%\CodexStatusbar\debug.log` : celui-ci peut contenir des titres de chats et des chemins locaux, ne pas le publier tel quel.

## Construire les sources

Installer le SDK .NET 10 sur Windows, puis exécuter :

```powershell
.\build.ps1
```

Le script compile, exécute les contrôles du parseur et des quotas, vérifie le déplacement de la vague sur deux rendus, puis génère l'EXE autonome dans `dist`. Pour lancer le résultat avec les réglages distribués :

```powershell
.\dist\CodexStatusbar.exe --settings "$PWD\portable\settings.json"
```

## Modifications de cette variante

- Fond dégradé et vague sinusoïdale animés, pause lorsque la fenêtre est masquée.
- Quotas hebdomadaire et cinq heures identifiés par leur durée, quel que soit leur emplacement dans les données.
- Contexte utilisé ajouté à la ligne de statistiques.
- Affichage limité à la saisie visible, masquage lorsqu'une fenêtre la recouvre.
- Placement au-dessus de la saisie et largeur adaptée au texte disponible.
- Autotest du thème rendu indépendant de la configuration personnelle de la machine.

## Licences et attribution

Sous licence MIT. Voir [LICENSE](LICENSE), [LICENSE-codex-token-overlay](LICENSE-codex-token-overlay) et [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md). Les auteurs et attributions des projets d'origine sont conservés. Cette distribution est une variante modifiée, et non une publication officielle du projet amont.
