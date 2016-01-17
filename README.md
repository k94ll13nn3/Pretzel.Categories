# Pretzel.Categories

[![Build status](https://ci.appveyor.com/api/projects/status/srtnat4hbs1xqr8m?svg=true)](https://ci.appveyor.com/project/k94ll13nn3/pretzel-categories)
[![Release](https://img.shields.io/github/release/k94ll13nn3/Pretzel.Categories.svg)](https://github.com/k94ll13nn3/Pretzel.Categories/releases/latest)

----

:warning: At this time (January 16th), this is based on an unstable version of Pretzel ([this version](https://ci.appveyor.com/project/laedit/pretzel/build/0.3.1-ci.18%20(Build%20311)/artifacts)).

-----

A category and tag page generator for Pretzel

This is a plugin for the static site generation tool [Pretzel](https://github.com/Code52/pretzel).

### Category Pages

Not implemented.

### Tag Pages

This plugin will automatically generate tag pages during build.

This can be disabled by using the `-ntag` option with the `taste` or `bake` command.

You can specify a layout by setting the `tag_pages_layout` configuration key (it uses `layout` by default).

### Installation

Download the [latest unstable release](https://ci.appveyor.com/project/k94ll13nn3/pretzel-categories) and copy `Pretzel.Categories.dll` to the `_plugins` folder at the root of your site folder (you may have to **unblock** the file).

<!---
Download the [latest release](https://github.com/k94ll13nn3/Pretzel.Categories/releases/latest) and copy `Pretzel.Categories.dll` to the `_plugins` folder at the root of your site folder (you may have to **unblock** the file).
-->