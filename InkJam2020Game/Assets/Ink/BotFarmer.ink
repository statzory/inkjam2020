/**
 * Cyberian Bot Farmer!!!!
 * 2018 Ændrew Rininsland (@aendrew)
 */
 
-> Beginning
 
// All of our filthy, disgusting global variables lololol
VAR canFarmFocebork = false
VAR canFarmTwurtur = true
VAR twManagers = 0
VAR fbManagers = 0
VAR totalFBBots = 0
VAR totalTWBots = 0
VAR totalRubels = 0
VAR totalFBDudes = 0
VAR totalTWDudes = 0
VAR fbManagerRate = 3
VAR twManagerRate = 6
VAR fbDudeRate = 3
VAR twDudeRate = 5
VAR twBotRate = 3
VAR fbBotRate = 8
CONST MAX_BOTS = 760000000 // INT_MAX in C++/Ink is 2147483647 lol.

=== function farmhouseType ===
{   
    - totalRubels < 1000:
        ~ return "shanty"
    - totalRubels < 10000:
        ~ return "decrepit"
    - totalRubels < 100000:
        ~ return "modest"
    - totalRubels < 1000000:
        ~ return "swank"
    - else:
        ~ return "incredible"
}
    
=== function totalBots ===
~ return totalFBBots + totalTWBots
   
=== function totalDudes ===
~ return totalFBDudes + totalTWDudes
    
=== function totalManagers ===
~ return twManagers + fbManagers
    
=== function fbIncome ===
~ return totalFBBots * fbBotRate
    
=== function twIncome ===
~ return totalTWBots * twBotRate

=== function tick ===
~ totalFBDudes += fbManagers * fbManagerRate
~ totalTWDudes += twManagers * twManagerRate
~ totalFBBots += fbDudeRate * totalFBDudes
~ totalTWBots += twDudeRate * totalTWDudes
~ totalRubels += twIncome() + fbIncome()
Currently you have ₽{totalRubels}, {totalBots()} total bots, {totalDudes()} dudes and {totalManagers()} dude managers working for you.
~ return totalBots() > MAX_BOTS



== Beginning ==
# background: beginning
You have inherited a farm from your uncle {~Jim|John|James|Vladimir}.
Gazing across the frozen wasteland, you doubt anything will grow here. 
Luckily, you have decent Internet connectivity and have been told there's a growing industry for social media consultants in the area...
* [Go to your farm]
    You head towards your farmhouse to plan what to do.
    -> WhatToDo
    
    
    
== WhatToDo ==
# background: farm
{ tick() == true: -> End }
You arrive back at your { farmhouseType() } farmhouse and ponder your next move.

You have a few options here:
+ [Farm a bot]
  -> FarmABot
+ [Go to The City]
  -> GoToCity
  
  
    
== FarmABot ==
# background: barn

{ tick() == true: -> End }

+ { canFarmFocebork } [Farm a Focebork bot]
    You farm a Focebork bot
    ~ totalFBBots += 1
    -> WhatToDo
+ { canFarmTwurtur } [Farm a Twurtur bot]
    You farm a Twurtur bot
    ~ totalTWBots += 1
    -> WhatToDo
+ Go back home
    ->WhatToDo    

== GoToCity ==
# background: city

{ tick() == true: -> End }You make it to The City.

You have ₽{totalRubels} to spend.

What do you want to do?
+ [Hire somebody to make some bots]
    You go to the local tavern
    -> LocalTavern
+ [Buy a recipe]
    You go to Bot Recipes R Us
    -> BotRecipesRUs
+ [Go home to your farm]
    You go home
    -> WhatToDo
    
    
    
== BotRecipesRUs ==
# background: store

{ tick() == true: -> End }

The proprietor of Bot Recipes R Us welcomes you into his establishment.

"Welcome! Welcome!!!"

"I have the finest bot recipes!"

You have ₽{totalRubels} to spend.

* { totalRubels >= 90000 and not canFarmFocebork } [Buy a Focebork recipe (₽90000)]
    ~ totalRubels -= 90000 
    ~ canFarmFocebork = true
    You buy a Focebork recipe.
    -> GoToCity
+ [Go back to the city.]
    { totalRubels < 90000: Your broke ass can't afford anything anyway. }
    -> GoToCity
    
    
    
== LocalTavern ==
# background: bar

{ tick() == true: -> End }

A motley crew of surly men sit drinking vodka.

You can:
+ { totalRubels >= 10000 } [Hire a Dude Manager]
    You can hire the following dude managers:
    ++ { canFarmFocebork && totalRubels >= 100000 } [Hire a Focebork dude manager (₽100000)]
        You hire {~Dmitri|Boris|Ralph}
        ~ fbManagers += 1
        ~ totalRubels -= 100000
    ++ { canFarmTwurtur && totalRubels >= 10000 } [Hire a Twurtur dude manager (₽10000)]
        You hire {~Dmitri|Boris|Ralph}
        ~ twManagers += 1
        ~ totalRubels -= 10000
+ { totalRubels >= 100} Hire a Dude
    You can hire the following Dudes:
    ++ { canFarmFocebork && totalRubels >= 1000 } [Hire a Focebork Dude (₽1000)]
        You hire {~Dmitri|Boris|Ralph}
        ~ totalFBDudes += 1
        ~ totalRubels -= 1000
    ++ { canFarmTwurtur && totalRubels >= 100 } [Hire a Twurtur Dude (₽100)]
        You hire {~Dmitri|Boris|Ralph}
        ~ totalTWDudes += 1
        ~ totalRubels -= 100
+ [Go back to the city.]
- You leave the tavern.
    ->GoToCity
    
    

== End ==
# background: ending

IT'S OVERRRRR CONGRATS GO HOME
-> END