# Coins Incremental

A first-person coin pusher game with an idle/incremental economy built on top of it. You walk up to an arcade machine, drop coins onto a physics-driven pusher shelf, and use what falls off the edge to buy upgrades, passive income, and eventually prestige for permanent multipliers.

Think of it as a coin-pusher arcade cabinet crossed with a Cookie Clicker-style progression loop, except you're actually standing in front of the machine instead of clicking a button on a menu.

This is a work in progress. There's no build, no license file, and no set release version yet — just the Unity project itself.

## What it is

The game is split into two loops that feed into each other:

- **The machine itself.** A dropper slides along a rail above a shelf that sweeps back and forth. You drop coins onto the shelf and physics decides whether they get pushed off the edge into the payout zone. Nothing here is faked — it's real Rigidbody physics.
- **The economy around it.** Money from the machine buys upgrades (better coin value, faster drops, higher crit chance, etc.), passive income sources that keep earning while you're away, and eventually lets you prestige — reset your progress in exchange for a permanent multiplier.

You're not stuck in a menu the whole time either. The game world has a small first-person section you walk around in, and you approach the coin machine and press E to actually start playing it, similar to how you'd walk up to an arcade cabinet.

## Features

- Physically simulated coin pusher — coins are real Rigidbodies, the pusher shelf moves on an eased back-and-forth cycle, and payouts depend on which coins actually get pushed into the reward zone
- First-person controller to walk around the space, with sprint, stamina, head bob and footstep sounds
- Five coin rarities (Normal, Critical, Lucky, Golden, Jackpot) with independent, upgradeable drop chances and different payout multipliers
- A combo system that ramps up your multiplier the more you chain payouts together before it resets
- An upgrade shop with exponential cost scaling per level (coin value, drop cooldown, crit chance, movement speed, and others)
- Passive income "businesses" you can buy that earn money automatically, even when you're not at the machine — Piggy Bank, Coin Jar, Cash Register, ATM, and Small Shop so far
- A visible tower/structure in the world that upgrades itself as your lifetime earnings pass certain thresholds (Car, House, Skyscraper, Falcon 9)
- Prestige system — reset your run once you've earned enough lifetime money, in exchange for tokens that permanently boost future earnings
- Local save file with auto-save every 30 seconds, plus offline earnings calculated from how long you were away (capped at a configurable number of hours)
- A custom big-number type for when your numbers get out of hand (formats up to the quindecillion range)

## Controls

Walking around:

- `WASD` or arrow keys to move
- Mouse to look
- Hold `Shift` to sprint (drains stamina, regenerates when you stop)
- `E` to interact with the coin machine

At the machine:

- Move the mouse to slide the dropper left and right (or use `A`/`D`, arrow keys)
- Left click to drop a coin, subject to a cooldown you can upgrade
- `Esc` to step away from the machine and go back to walking around

## How progression works

**Coin types.** Every coin you drop rolls a type: Normal (1x), Critical (2x, ~5% base chance), Lucky (3x, ~3% base chance), Golden (5x, ~1% base chance), or Jackpot (20x, a rarer bonus roll inside jackpot-flagged reward zones). All of these chances scale with upgrades.

**Combos.** Landing payouts back-to-back within a short window builds a combo multiplier. Stop scoring for a couple seconds and it resets.

**Upgrades.** Bought through a standard shop UI, each one scales in cost as `BaseCost * CostGrowth^Level`, capped at a max level. Current upgrade paths cover coin value, coin size/weight, drop cooldown, movement speed, pusher speed, combo multiplier, and the various rarity chances.

**Passive income.** These are the "businesses" — buy them once and they generate money per second automatically, whether you're at the machine or not. Costs and income both scale exponentially per level. In the current build, roughly cheapest to most expensive:

- Piggy Bank — 50 to start, 1/sec
- Coin Jar — 250 to start, 5/sec
- Cash Register — 1,000 to start, 20/sec
- ATM — 5,000 to start, 100/sec
- Small Shop — 25,000 to start, 600/sec

**The tower.** There's a physical object in the scene that changes/grows as your *lifetime* earnings (not your current balance) cross certain thresholds — Car at 10K, House at 100K, and Skyscraper/Falcon 9 at 1M. It's basically a visual trophy case for how far you've gotten.

**Prestige.** Once your lifetime earnings pass the threshold (1,000,000 by default), you can reset your money, upgrades, passive income, and tower progress in exchange for prestige tokens. Each token adds a permanent +10% to everything you earn afterward, and the number of tokens you get scales with the square root of how far past the threshold you went, so overshooting matters.

**Saving.** Everything gets written to a local JSON save file every 30 seconds and again whenever you prestige. When you come back after being away, it works out how long you were gone and pays out passive income for that time (up to a max offline cap).

## Tech stack

- Unity 6 (built and tested on `6000.3.10f1`)
- Universal Render Pipeline
- Unity's Input System package (not the legacy input manager)
- UGUI + TextMesh Pro for UI
- C#

A few things worth knowing if you're poking around the code: managers use a small generic `Singleton<T>` base class, systems talk to each other through a static `EventBus` instead of direct references, upgrades/passive assets/tower stages are all defined as ScriptableObjects pulled together through one `GameDatabase`, and stats are computed through a modifier stack (flat / percent / multiplier) rather than hardcoded values. There's also a custom `BigNumber` struct (mantissa + exponent, normalized in base 1000) for when regular doubles aren't enough.

## Getting it running

You'll need Unity Hub and Unity **6000.3.10f1** (or a compatible Unity 6.x build) installed.

```bash
git clone https://github.com/Piyro/Coins-Incremental.git
cd Coins-Incremental
```

Open Unity Hub, click Add → Add project from disk, and point it at the folder. Hub should offer to install the matching editor version if you don't already have it. Let Unity import and resolve packages, then open `Assets/Scenes/SampleScene.unity` and hit Play.

To build a standalone version instead: File → Build Settings, make sure `SampleScene` is in the build list, pick your platform, and build.

## Project layout

```
Coins-Incremental/
├── Assets/
│   ├── Scripts/
│   │   ├── CoinSystem/        coin physics, pusher, dropper, spawner, rewards, combo logic
│   │   ├── Core/               game session state, tick system, machine enter/exit, save wiring
│   │   ├── Data/                player progress/settings/stats models
│   │   ├── Debug/               dev-only cheat keys (F1-F3) for testing the economy
│   │   ├── Economy/             money and upgrade management
│   │   ├── Events/               event payloads used by the EventBus
│   │   ├── Game Managers/       game manager, constants, database, installer
│   │   ├── Math/                 BigNumber + formatter
│   │   ├── Passive Income/       passive asset definitions and manager
│   │   ├── Player Controller/    first-person controller
│   │   ├── Prestige/              prestige controller and node definitions
│   │   ├── Save System/          JSON save/load
│   │   ├── StatType/              stat + modifier system
│   │   ├── Tower Stage/           tower growth logic
│   │   ├── UI/                    money/upgrade/business/prestige display scripts
│   │   └── Visuals/               floating money text
│   ├── ScriptableObjects/       upgrade, passive asset, tower stage, and database assets
│   ├── Scenes/                   SampleScene.unity (the only scene right now)
│   └── Decrepit Dungeon LITE/    third-party environment art pack
├── Packages/
├── ProjectSettings/
└── Coins Incremental.slnx
```

## Screenshots

<img width="1672" height="941" alt="Ekran görüntüsü 2026-08-10 081537" src="https://github.com/user-attachments/assets/f7da8633-02bc-479d-bf87-ae89d3465279" />


## What's probably next

Nothing here is an official roadmap, this is just stuff that's clearly half-built already based on what's in the code:

- A proper prestige tree — `PrestigeNodeDefinition` already supports branching, repeatable nodes with costs and bonuses, but nothing uses it yet beyond the flat +10%-per-token multiplier
- Multi-drop and auto-drop — both exist as upgrade/stat types but aren't hooked up in the spawner logic
- Bounce chance — same story, defined but not implemented anywhere
- More tower stages — right now it's just Car, House, and Skyscraper/Falcon 9, which caps out fast
- Cleaning up the save system — there are currently two `SaveManager` classes in the project (one's an empty leftover stub in `Core/Save`, the real one lives in `Save System`), worth merging or removing the dead one
- An actual license and contribution docs, since neither exists yet

## License

There's no LICENSE file in this repo at the moment, so by default all rights are reserved — the code isn't open for reuse or redistribution until that changes. If you're the maintainer and want this open source, adding an MIT or similar license file would clear that up.

## Credits

Built by [Piyro](https://github.com/Piyro) in Unity 6. Uses the Decrepit Dungeon LITE asset pack for some of the environment art, and TextMesh Pro for UI text.

## Contributing

No formal contributing guide exists yet. If you want to help out, fork it, make your changes against Unity 6000.3.10f1, and open a PR explaining what you did. Since there's no license attached yet, it's probably worth checking in with the maintainer before putting in a lot of work.
