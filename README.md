# Pretzel.Categories

[![Build status](https://ci.appveyor.com/api/projects/status/srtnat4hbs1xqr8m?svg=true)](https://ci.appveyor.com/project/k94ll13nn3/pretzel-categories)
[![Release](https://img.shields.io/github/release/k94ll13nn3/Pretzel.Categories.svg)](https://github.com/k94ll13nn3/Pretzel.Categories/releases/latest)

A category and tag pages generator for Pretzel (version 0.4 or greater)

This is a plugin for the static site generation tool [Pretzel](https://github.com/Code52/pretzel).

### Category Pages

This plugin will automatically generate category pages during build. Each category page is saved as`_site/category/<slugified-category-name>/index.html`.

This can be disabled by using the `-ncategory` option with the `taste` or `bake` command.

You can specify a layout by setting the `category_pages_layout` configuration key in the config.yml (it uses `layout` by default).

### Tag Pages

This plugin will automatically generate tag pages during build. Each tag page is saved as`_site/tag/<slugified-tag-name>/index.html`.

This can be disabled by using the `-ntag` option with the `taste` or `bake` command.

You can specify a layout by setting the `tag_pages_layout` configuration key in the config.yml (it uses `layout` by default).

### Installation

Download the [latest release](https://github.com/k94ll13nn3/Pretzel.Categories/releases/latest) and copy `Pretzel.Categories.dll` to the `_plugins` folder at the root of your site folder (you may have to **unblock** the file).