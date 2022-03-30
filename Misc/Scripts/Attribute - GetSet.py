from typing import Callable
from pyperclip import copy, paste

# Formata o código inicialmente provido
# Elimina espaços desnecessários e separa o código em linhas
# Retorna uma lista com as linhas de atributos
def codeFormatter(code:str) -> list:
    while (code.find('\n ') > 0):
        code = code.strip().replace('\n ', '\n').replace(';', '').replace('\r', '')
    codigo = code.split('\n')
    return codigo


# Remove os atributos static
def staticAttributeRemover(codigo:list) -> list:
    temp = []
    # Encontra as linhas com atributos static
    for linha in range(len(codigo)):
        code = codigo[linha].split(' ')
        for word in code:
            if word.lower() == 'static':
                temp.append(codigo[linha])
                break

    # Remove as linhas encontradas
    for linha in temp:
        codigo.remove(linha)
    return codigo


# Remove o nível de proteção dos atributos
def protectionLevelRemover(codigo:list) -> list:
    for i in range(len(codigo)):
        code = codigo[i].split(' ')
        if code[0].lower() in ['private', 'protected', 'public']:
            code = code[1:]
        codigo[i] = ' '.join(code)
    return codigo


# Recebe uma sequência de atributos e retorna os respectivos GET & SET
def attributesToGetSet(codigo:str, GetSet:Callable, case:Callable) -> None:
    # Formata o código recebido
    codigo = codeFormatter(codigo)

    # Remove atributos 'static' do código
    codigo = staticAttributeRemover(codigo)

    # Remove o nível de proteção dos atributos
    codigo = protectionLevelRemover(codigo)

    # Cria uma string com os métodos Get e Set
    codigo = GetSet(codigo, case)

    # Copia a string para o clipboard (CTR + C / CTRL + V)
    copy(codigo)


# Retorna uma string formatada como 'snake_case'
def snake_case(atributo:str) -> str:
    attr = []
    word = 0
    atributo = atributo.replace('\n', '')
    for char in range(len(atributo)):
        if (atributo[char] == atributo[char].upper() or atributo[char] == '_') and char != 0:
            attr.append(atributo[word:char])
            word = char
        if char == len(atributo) - 1:
            attr.append(atributo[word:])
    atributo = '_'.join(attr)
    while (True):
        atributo = atributo.replace('__', '_')
        if (atributo.find('__') < 0):
            break
    while atributo[0] == '_':
        atributo = atributo[1:]
    while atributo[-1] == '_':
        atributo = atributo[:-1]
    return atributo.lower()


# Retorna uma string formatada como 'camelCase'
def camelCase(atributo:str) -> str:
    atributo = snake_case(atributo).split('_')
    for i in range(len(atributo)):
        atributo[i] = atributo[i].capitalize()
    atributo[0] = atributo[0].lower()
    return ''.join(atributo)


# Retorna uma string formatada como 'PascalCase'
def PascalCase(atributo:str) -> str:
    atributo = snake_case(atributo).split('_')
    for i in range(len(atributo)):
        atributo[i] = atributo[i].capitalize()
    return ''.join(atributo)


# Retorna um codigo no formato
#Get { ... }
#Set { ... }
#Get { ... }
#Set { ... }
def GetSetGetSet(code:str, case:Callable) -> str:
    codigo = []
    for linha in code:
        words = linha.split(' ')
        get = case('get_' + words[1])
        set = case('set_' + words[1])
        codigo.append(f'\tpublic {words[0]} {get}()\n\t{"{"}\n\t\treturn {words[1]};\n\t{"}"}\n')
        codigo[-1] += f'\tpublic void {set}({words[0]} {words[1]})\n\t{"{"}\n\t\tthis.{words[1]} = {words[1]};\n\t{"}"}'
    return '\n\n'.join(codigo)


# Retorna um codigo no formato
#Get { ... }
#Get { ... }
#Set { ... }
#Set { ... }
def GetGetSetSet(code:str, case:Callable) -> str:
    codigo = []
    for linha in code:
        words = linha.split(' ')
        get = case('get_' + words[1])
        codigo.append(f'\tpublic {words[0]} {get}()\n\t{"{"}\n\t\treturn {words[1]};\n\t{"}"}\n')
    codigo.append('\n')
    for linha in code:
        words = linha.split(' ')
        set = case('set_' + words[1])
        codigo.append(f'\tpublic {words[0]} {set}({words[0]} {words[1]})\n\t{"{"}\n\t\tthis.{words[1]} = {words[1]};\n\t{"}"}\n')
    return ''.join(codigo)[:-1]


def main():
    attributesToGetSet(paste(), GetSetGetSet, camelCase)


if __name__ == '__main__':
    main()
