VAR memories_visited = 0
VAR has_compassion = true
VAR has_curiosity = true

-> the_summoning

=== function came_from(-> x) ===
	~ return TURNS_SINCE(x) == 0

=== the_summoning ===
# background:study
The books are prepared. The words are memorized.The offering has been given.
Is the month right?
* [Of course]
* [The stars are aligned]
* [I've cleared my schedule for this]
- Is the day accurate?
* [Yes]
* [The days of the week have blended together]
    A nervous glance at your calander assures you that you have not made a mistake.
* [It could not be any other]
- Has the hour arrived?
* [I know it has]
* [It is written in the shadows]
* [I should look at the clock]
    It tells you the hour is right.
- Then the summoning can begin.
You say the words. You renounce God. You call out his name.
* Mephistopheles
- He appears before you. In the flesh. In fur. In smoke.
<color=red>Who calls me?</color>
* [A humble conjurer]
    <color=red>So you can show reverence? Good!</color>
* [A wise conjurer]
    <color=red>If you were wise you would not be here.</color>
* [Dreadful shape!]
    <color=red>How rude.</color>
- <color=red>You are here because you require my services. My knowledge, my magic, and my occasional intervention.
What would you with that kind of power?</color>
* [I could understand everthing! I could do what science could not]
* [So much is broken out there. If I had power, I could fix it]
* [I will live and go on adventures! I could have a real conversation with a stranger!]
- <color=red>That sounds wonderful, and I would be thrilled to aid you in your achievement.
Of course, we must discuss the matter of price, but that is what the negotiation is for.</color>
* [Was my offering not enough?]
    <color=red>It was delicious, but for my services I will require a little more. I will require your soul.</color>
* [...]
* [Of course, let us begin]
- <color=red>Luckily for you, I am generous. I will not demand your entire soul. A few choice cuts will do.
For that, I will need to examine you. Appraise the merchandise.
Let me see..
Where should we go first?</color>
- (choice)
* [The party]
    ->the_house_party
* [The study]
    ->alone_in_study
* [His apartment]
    ->gregs_apartment
* ->
    ->END

=== the_house_party ===
# background:party
Party!
~ memories_visited++
->the_summoning.choice

=== gregs_apartment ===
# background:apartment
I was lying on his bed.
Greg was in the living room.
~ memories_visited++
->the_summoning.choice

=== alone_in_study ===
# background:study
Study!
~ memories_visited++
->the_summoning.choice

