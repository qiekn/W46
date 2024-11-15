## VIM
```bash
vim
:args **/*.cs
arge
qa
:g/xxxxxxx/d
:if getline(1) =~ '^$' | 1delete | endif | w | bd
q
:argdo norm @a
:waq
```