PGDMP     9                    }            d5    15.5    15.5                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    33837    d5    DATABASE     v   CREATE DATABASE d5 WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE d5;
                postgres    false            �            1259    33838    material    TABLE     �   CREATE TABLE public.material (
    material text NOT NULL,
    type text,
    price text,
    kolvo text,
    min text,
    kolvo_up text,
    ed text
);
    DROP TABLE public.material;
       public         heap    postgres    false            �            1259    33843    post    TABLE     r   CREATE TABLE public.post (
    post text NOT NULL,
    tip text,
    inn text,
    reit integer,
    data text
);
    DROP TABLE public.post;
       public         heap    postgres    false            �            1259    33848    postavki    TABLE     `   CREATE TABLE public.postavki (
    id_pos integer NOT NULL,
    material text,
    post text
);
    DROP TABLE public.postavki;
       public         heap    postgres    false            �            1259    33853    postavki_id_pos_seq    SEQUENCE     �   CREATE SEQUENCE public.postavki_id_pos_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.postavki_id_pos_seq;
       public          postgres    false    216                       0    0    postavki_id_pos_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.postavki_id_pos_seq OWNED BY public.postavki.id_pos;
          public          postgres    false    217            �            1259    33854    tip    TABLE     3   CREATE TABLE public.tip (
    tip text NOT NULL
);
    DROP TABLE public.tip;
       public         heap    postgres    false            �            1259    33859    type    TABLE     D   CREATE TABLE public.type (
    type text NOT NULL,
    proc text
);
    DROP TABLE public.type;
       public         heap    postgres    false            u           2604    33864    postavki id_pos    DEFAULT     r   ALTER TABLE ONLY public.postavki ALTER COLUMN id_pos SET DEFAULT nextval('public.postavki_id_pos_seq'::regclass);
 >   ALTER TABLE public.postavki ALTER COLUMN id_pos DROP DEFAULT;
       public          postgres    false    217    216                      0    33838    material 
   TABLE DATA           S   COPY public.material (material, type, price, kolvo, min, kolvo_up, ed) FROM stdin;
    public          postgres    false    214   =                 0    33843    post 
   TABLE DATA           :   COPY public.post (post, tip, inn, reit, data) FROM stdin;
    public          postgres    false    215   �                 0    33848    postavki 
   TABLE DATA           :   COPY public.postavki (id_pos, material, post) FROM stdin;
    public          postgres    false    216   %!                 0    33854    tip 
   TABLE DATA           "   COPY public.tip (tip) FROM stdin;
    public          postgres    false    218   �$                 0    33859    type 
   TABLE DATA           *   COPY public.type (type, proc) FROM stdin;
    public          postgres    false    219   +%                  0    0    postavki_id_pos_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.postavki_id_pos_seq', 80, true);
          public          postgres    false    217            w           2606    33866    material material_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.material
    ADD CONSTRAINT material_pkey PRIMARY KEY (material);
 @   ALTER TABLE ONLY public.material DROP CONSTRAINT material_pkey;
       public            postgres    false    214            y           2606    33868    post post_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.post
    ADD CONSTRAINT post_pkey PRIMARY KEY (post);
 8   ALTER TABLE ONLY public.post DROP CONSTRAINT post_pkey;
       public            postgres    false    215            {           2606    33870    postavki postavki_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.postavki
    ADD CONSTRAINT postavki_pkey PRIMARY KEY (id_pos);
 @   ALTER TABLE ONLY public.postavki DROP CONSTRAINT postavki_pkey;
       public            postgres    false    216            }           2606    33872    tip tip_pkey 
   CONSTRAINT     K   ALTER TABLE ONLY public.tip
    ADD CONSTRAINT tip_pkey PRIMARY KEY (tip);
 6   ALTER TABLE ONLY public.tip DROP CONSTRAINT tip_pkey;
       public            postgres    false    218                       2606    33874    type type_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.type
    ADD CONSTRAINT type_pkey PRIMARY KEY (type);
 8   ALTER TABLE ONLY public.type DROP CONSTRAINT type_pkey;
       public            postgres    false    219            �           2606    33875    material material_type_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.material
    ADD CONSTRAINT material_type_fkey FOREIGN KEY (type) REFERENCES public.type(type) ON UPDATE CASCADE ON DELETE CASCADE;
 E   ALTER TABLE ONLY public.material DROP CONSTRAINT material_type_fkey;
       public          postgres    false    3199    214    219            �           2606    33880    post post_tip_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.post
    ADD CONSTRAINT post_tip_fkey FOREIGN KEY (tip) REFERENCES public.tip(tip) ON UPDATE CASCADE ON DELETE CASCADE;
 <   ALTER TABLE ONLY public.post DROP CONSTRAINT post_tip_fkey;
       public          postgres    false    218    215    3197            �           2606    33885    postavki postavki_material_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.postavki
    ADD CONSTRAINT postavki_material_fkey FOREIGN KEY (material) REFERENCES public.material(material) ON UPDATE CASCADE ON DELETE CASCADE;
 I   ALTER TABLE ONLY public.postavki DROP CONSTRAINT postavki_material_fkey;
       public          postgres    false    216    214    3191            �           2606    33890    postavki postavki_post_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.postavki
    ADD CONSTRAINT postavki_post_fkey FOREIGN KEY (post) REFERENCES public.post(post) ON UPDATE CASCADE ON DELETE CASCADE;
 E   ALTER TABLE ONLY public.postavki DROP CONSTRAINT postavki_post_fkey;
       public          postgres    false    3193    216    215               R  x��T[n�0��N���H��u��vڤ��A�|��?�)�:1�X�z��:�zؒ����DK��ٙ��**�@��5��_�%���~Eې�x�������ʯ�q��f��"�\$�8R���PV7��9�6R&��Һ��/(���_��-l3��&�&�����=����O��|�i:!F����j ��}�+��u�RJ�i���xT�ϸ��丏q][����[�
!��Z`.���xݻ>�6:w=�������:��f�m�r��a�wh�Id��j�-��Eݘ�|0�_
�F+�h由�1{��yB,k�"�bM�e�v�rC,���B?����<��_�﵊3���d���m-�mp�g�ˊ�Qpb`s憛�~=�O"ʻf�i6H�`	���)U�E��$OO�8	�Z"\�DM��_b�S�Cc�I�̞���!��!�ڊ[���	�,���&U�Z�PvG�Ga�S&��S���w
_Hv��R�+ndZ�4Qq2�.�"�S|�3﬘gR'`��GC�%��?M�xP�e��d&c�8���0#�usl�G���X�rz�d��1���VG;��>��`�         v  x�mT[NA��=�O�����%��M�(H$�PB )�����W�Q�g622���ꮪY���|λ<�W���<�=��{�'��&����oU�q�SlTRF�dj��W���7���_�GƤmJ���"[�F���+���ϼ����Y>��^�L�|TrVۆ�� 1��_�����(j�Lk��T� �M>+Zk~A|.@��g�Oy������3,pd9�cH1yJ�,j:+~�`���c�穬��O1$5�\�N52X��������y,Zr��,a|RV������⿘n˗���Pcȵ��j$���5�/����̸��+~���oJtsp�ă^�zkl��J�D5~�be�-��`���}�3��  r{I��L��_�o�yfh����o��7��uA�g6�i�
��j���:�)(��7&z����ˣ�}]gH�!D2!Jt��j(�_ˊ��v���)8�F���)���{(<�P��ko�

��D)���J^Bh{qݗv�j��|���Cg���<�?0No�1�a���Z&�%Wū����%$�Ԣ@݋��J�y��&���Bk�R�
]\⭘��q��y7B>rZ��qQEW�OuUU� Q=m9         �  x��Xmn�@��=�/ ����p�� �j) ��"�"$~�iҦI�^a�F�c�����h{wg߼y�f���s=��~Mk��(���T�G�#K�Ҍ6*�d|)hC+�z�#��c��[^R�ѧ>�NhA35}�ix�b�]ʬS
z���幊=��%�m�hI8���^���|z�%����#��4�#>��C�ԣx3,�{�Y`]��[��&�L @��T�S6%>ė�9�2mL�01�����;\t!a���FO�ȣ/q���H�<�1q[m�����s.�ds����p��$�������F�>��cB.�xhV1�XB+�|��B׺b�*��\גʓ��ҧ*��؁&�$����>a�:�6��^� ��}^�C��}���CB�G��x[a"w�<ړ�D�N��x��T8`�$>PMD^��J�r��%	���Y�b#;�Z����L3[��&G�y�Z�h|����=嚩�KN�6�ĭaV˼�C{-�5��LL��璀0f�H�I�9$Z�\���焸��V�(`��������/i��v���"��ō_qݤ&��p���D(_�u���˄#�xA[ҙ�÷�hذ�+���Z�r*�|�=h<h\�NMŠ�8��~?�8:��>��8�����d��%{���bqb���Gũ��B� ՝���#}nSg�5&�6u�mhY��g# V��R%����2q
��5ؖݖO��|rsG(IPk%,��J�J͠9	M�r`��|nݐכ��C��Χ��-}���6��5�w�}&�Zk5`:����M:��<׀	��^2�-�� �b�*u�5a�1��;�����q�6ֿ!���}�A멭9����Q�[_:�g��g����[���ET�u�&��Oy6/ �F����8��ӟ�ĥ���ƭ�?����/0�Н��(F�������ߋ�:{�R)��T�u            x��0��.�A��a<���� Z �         y   x����0k<B!�m&8E��Hda%��m�s�u�t�����"E~�k�F���Bbְj��v���4z1�����DfR���.��?�u�u>���jx������s�lm�v!�?�SN�     