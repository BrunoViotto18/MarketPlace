from pyperclip import paste, copy

def replacer(txt:str, old:str, new:str) -> None:
    txt = txt.replace(old, new).replace('\r', '')
    copy(txt)


def main():
    replacer(paste(), '"', "'")


if __name__ == '__main__':
    main()
