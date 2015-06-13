﻿using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using com.xamarin.AndroidIconics.Typefaces;

namespace com.xamarin.AndroidIconics
{
  public class Octicons : ITypeface
  {
    private const string TTF_FILE = "octicons.ttf";

    private static Typeface _typeface;

    private static Dictionary<string, char> _chars;

    public IIcon GetIcon(string key)
    {
      return new OcticonsIcon((Icon) Enum.Parse(typeof (Icon), key, true));
    }

    public Dictionary<string, char> Characters
    {
      get
      {
        if (
          _chars == null)
        {
          _chars = new Dictionary<string, char>();

          foreach (var name in Enum.GetNames(typeof (Icon)))
          {
            var value = (Icon) Enum.Parse(typeof (Icon), name);
            _chars.Add(name, (char) value);
          }
        }
        return _chars;
      }
    }

    public string MappingPrefix
    {
      get { return "oct"; }
    }

    public string FontName
    {
      get { return "Octicons"; }
    }

    public string Version
    {
      get { return "2.2.0"; }
    }

    public int IconCount
    {
      get { return _chars.Count; }
    }


    public ICollection<string> Icons
    {
      get { return Enum.GetNames(typeof (Icon)); }
    }

    public string Author
    {
      get { return "GitHub"; }
    }

    public string Url
    {
      get { return "https://github.com/github/octicons"; }
    }

    public string Description
    {
      get { return "GitHub's icon font https://octicons.github.com/"; }
    }

    public string License
    {
      get { return "SIL OFL 1.1"; }
    }

    public string LicenseUrl
    {
      get { return "http://scripts.sil.org/OFL"; }
    }

    public Typeface GetTypeface(Context context)
    {
      if (_typeface == null)
      {
        try
        {
          _typeface = Typeface.CreateFromAsset(context.Assets, /*"fonts/" +*/ TTF_FILE);
        }
        catch (Exception)
        {
          return null;
        }
      }
      return _typeface;
    }

    public enum Icon
    {
      //Octicons
      // ReSharper disable InconsistentNaming
      oct_alert = '\uf02d',
      oct_alignment_align = '\uf08a',
      oct_alignment_aligned_to = '\uf08e',
      oct_alignment_unalign = '\uf08b',
      oct_arrow_down = '\uf03f',
      oct_arrow_left = '\uf040',
      oct_arrow_right = '\uf03e',
      oct_arrow_small_down = '\uf0a0',
      oct_arrow_small_left = '\uf0a1',
      oct_arrow_small_right = '\uf071',
      oct_arrow_small_up = '\uf09f',
      oct_arrow_up = '\uf03d',
      oct_beer = '\uf069',
      oct_book = '\uf007',
      oct_bookmark = '\uf07b',
      oct_briefcase = '\uf0d3',
      oct_broadcast = '\uf048',
      oct_browser = '\uf0c5',
      oct_bug = '\uf091',
      oct_calendar = '\uf068',
      oct_check = '\uf03a',
      oct_checklist = '\uf076',
      oct_chevron_down = '\uf0a3',
      oct_chevron_left = '\uf0a4',
      oct_chevron_right = '\uf078',
      oct_chevron_up = '\uf0a2',
      oct_circle_slash = '\uf084',
      oct_circuit_board = '\uf0d6',
      oct_clippy = '\uf035',
      oct_clock = '\uf046',
      oct_cloud_download = '\uf00b',
      oct_cloud_upload = '\uf00c',
      oct_code = '\uf05f',
      oct_color_mode = '\uf065',
      oct_comment_add = '\uf02b',
      oct_comment = '\uf02b',
      oct_comment_discussion = '\uf04f',
      oct_credit_card = '\uf045',
      oct_dash = '\uf0ca',
      oct_dashboard = '\uf07d',
      oct_database = '\uf096',
      oct_device_camera = '\uf056',
      oct_device_camera_video = '\uf057',
      oct_device_desktop = '\uf27c',
      oct_device_mobile = '\uf038',
      oct_diff = '\uf04d',
      oct_diff_added = '\uf06b',
      oct_diff_ignored = '\uf099',
      oct_diff_modified = '\uf06d',
      oct_diff_removed = '\uf06c',
      oct_diff_renamed = '\uf06e',
      oct_ellipsis = '\uf09a',
      oct_eye_unwatch = '\uf04e',
      oct_eye_watch = '\uf04e',
      oct_eye = '\uf04e',
      oct_file_binary = '\uf094',
      oct_file_code = '\uf010',
      oct_file_directory = '\uf016',
      oct_file_media = '\uf012',
      oct_file_pdf = '\uf014',
      oct_file_submodule = '\uf017',
      oct_file_symlink_directory = '\uf0b1',
      oct_file_symlink_file = '\uf0b0',
      oct_file_text = '\uf011',
      oct_file_zip = '\uf013',
      oct_flame = '\uf0d2',
      oct_fold = '\uf0cc',
      oct_gear = '\uf02f',
      oct_gift = '\uf042',
      oct_gist = '\uf00e',
      oct_gist_secret = '\uf08c',
      oct_git_branch_create = '\uf020',
      oct_git_branch_delete = '\uf020',
      oct_git_branch = '\uf020',
      oct_git_commit = '\uf01f',
      oct_git_compare = '\uf0ac',
      oct_git_merge = '\uf023',
      oct_git_pull_request_abandoned = '\uf009',
      oct_git_pull_request = '\uf009',
      oct_globe = '\uf0b6',
      oct_graph = '\uf043',
      oct_heart = '\u2665',
      oct_history = '\uf07e',
      oct_home = '\uf08d',
      oct_horizontal_rule = '\uf070',
      oct_hourglass = '\uf09e',
      oct_hubot = '\uf09d',
      oct_inbox = '\uf0cf',
      oct_info = '\uf059',
      oct_issue_closed = '\uf028',
      oct_issue_opened = '\uf026',
      oct_issue_reopened = '\uf027',
      oct_jersey = '\uf019',
      oct_jump_down = '\uf072',
      oct_jump_left = '\uf0a5',
      oct_jump_right = '\uf0a6',
      oct_jump_up = '\uf073',
      oct_key = '\uf049',
      oct_keyboard = '\uf00d',
      oct_law = '\uf0d8',
      oct_light_bulb = '\uf000',
      oct_link = '\uf05c',
      oct_link_external = '\uf07f',
      oct_list_ordered = '\uf062',
      oct_list_unordered = '\uf061',
      oct_location = '\uf060',
      oct_gist_private = '\uf06a',
      oct_mirror_private = '\uf06a',
      oct_git_fork_private = '\uf06a',
      oct_lock = '\uf06a',
      oct_logo_github = '\uf092',
      oct_mail = '\uf03b',
      oct_mail_read = '\uf03c',
      oct_mail_reply = '\uf051',
      oct_mark_github = '\uf00a',
      oct_markdown = '\uf0c9',
      oct_megaphone = '\uf077',
      oct_mention = '\uf0be',
      oct_microscope = '\uf089',
      oct_milestone = '\uf075',
      oct_mirror_public = '\uf024',
      oct_mirror = '\uf024',
      oct_mortar_board = '\uf0d7',
      oct_move_down = '\uf0a8',
      oct_move_left = '\uf074',
      oct_move_right = '\uf0a9',
      oct_move_up = '\uf0a7',
      oct_mute = '\uf080',
      oct_no_newline = '\uf09c',
      oct_octoface = '\uf008',
      oct_organization = '\uf037',
      oct_package = '\uf0c4',
      oct_paintcan = '\uf0d1',
      oct_pencil = '\uf058',
      oct_person_add = '\uf018',
      oct_person_follow = '\uf018',
      oct_person = '\uf018',
      oct_pin = '\uf041',
      oct_playback_fast_forward = '\uf0bd',
      oct_playback_pause = '\uf0bb',
      oct_playback_play = '\uf0bf',
      oct_playback_rewind = '\uf0bc',
      oct_plug = '\uf0d4',
      oct_repo_create = '\uf05d',
      oct_gist_new = '\uf05d',
      oct_file_directory_create = '\uf05d',
      oct_file_add = '\uf05d',
      oct_plus = '\uf05d',
      oct_podium = '\uf0af',
      oct_primitive_dot = '\uf052',
      oct_primitive_square = '\uf053',
      oct_pulse = '\uf085',
      oct_puzzle = '\uf0c0',
      oct_question = '\uf02c',
      oct_quote = '\uf063',
      oct_radio_tower = '\uf030',
      oct_repo_delete = '\uf001',
      oct_repo = '\uf001',
      oct_repo_clone = '\uf04c',
      oct_repo_force_push = '\uf04a',
      oct_gist_fork = '\uf002',
      oct_repo_forked = '\uf002',
      oct_repo_pull = '\uf006',
      oct_repo_push = '\uf005',
      oct_rocket = '\uf033',
      oct_rss = '\uf034',
      oct_ruby = '\uf047',
      oct_screen_full = '\uf066',
      oct_screen_normal = '\uf067',
      oct_search_save = '\uf02e',
      oct_search = '\uf02e',
      oct_server = '\uf097',
      oct_settings = '\uf07c',
      oct_log_in = '\uf036',
      oct_sign_in = '\uf036',
      oct_log_out = '\uf032',
      oct_sign_out = '\uf032',
      oct_split = '\uf0c6',
      oct_squirrel = '\uf0b2',
      oct_star_add = '\uf02a',
      oct_star_delete = '\uf02a',
      oct_star = '\uf02a',
      oct_steps = '\uf0c7',
      oct_stop = '\uf08f',
      oct_repo_sync = '\uf087',
      oct_sync = '\uf087',
      oct_tag_remove = '\uf015',
      oct_tag_add = '\uf015',
      oct_tag = '\uf015',
      oct_telescope = '\uf088',
      oct_terminal = '\uf0c8',
      oct_three_bars = '\uf05e',
      oct_thumbsdown = '\uf0db',
      oct_thumbsup = '\uf0da',
      oct_tools = '\uf031',
      oct_trashcan = '\uf0d0',
      oct_triangle_down = '\uf05b',
      oct_triangle_left = '\uf044',
      oct_triangle_right = '\uf05a',
      oct_triangle_up = '\uf0aa',
      oct_unfold = '\uf039',
      oct_unmute = '\uf0ba',
      oct_versions = '\uf064',
      oct_x = '\uf081',
      oct_zap = '\u26A1'
      // ReSharper enable InconsistentNaming
    }

    private class OcticonsIcon : IIcon
    {
      private readonly Icon _icon;

      protected internal OcticonsIcon(Icon icon)
      {
        _icon = icon;
      }

      public string GetFormattedName
      {
        get { return string.Format("{{{0}}}", _icon); }
      }

      public char GetCharacter
      {
        get { return (char) _icon; }
      }

      public string GetName
      {
        get { return _icon.ToString().ToUpperInvariant(); }
      }

      // remember the typeface so we can use it later
      private ITypeface _iconTypeface;

      public ITypeface GetTypeface
      {
        get { return _iconTypeface ?? (_iconTypeface = new Octicons()); }
      }
    }
  }
}